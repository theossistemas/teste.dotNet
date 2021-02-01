using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; 
using Theos.Livraria.Domain.Entity; 
using Theos.Livraria.Domain.Interface.Services;
using Theos.Livraria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theos.Livraria.Domain.Interface.Repository;
using Theos.Livraria.Domain.Model.Livro;
using Theos.Livraria.Application.Validators;
using Microsoft.Extensions.Configuration;

namespace Theos.Livraria.Application.Services
{
    public class LivroService : BaseService, ILivroService
    {
        protected readonly ILivroRepository _livroRepository;

        public LivroService(
                ILivroRepository livroRepository, 
                IMapper mapper, 
                ILogger<LivroService> logger,
                IConfiguration configuration) : 
            base (mapper, logger, configuration)
        {
            _livroRepository = livroRepository; 
        }


        public async Task<BaseResponse> Inserir(RequestLivro request)
        {
            try
            {
                _logger.LogInformation("Iniciando cadastro do livro.");

                var valid = await ValidarRequest(new LivroValidator(), request);
                if (valid != null)
                    return valid;

                var entity = _mapper.Map<Livro>(request);
                entity.DataCadastro = DateTime.Now;

                var response = await ObterStatusCode(
                     "Livro cadastrado com sucesso.",
                     StatusCodes.Status201Created,
                      _mapper.Map<ResponseLivro>(await _livroRepository.Inserir(entity)));

                _logger.LogInformation("Fim do cadastro do livro.");

                return response;
            }
            catch (Exception ex)
            {
                return await ObterStatusCode("Erro ao cadastrar o livro", StatusCodes.Status400BadRequest, null, ex);
            }
        }

        public async Task<BaseResponse> Atualizar(RequestLivro request)
        {
            try
            {
                _logger.LogInformation("Iniciando atualização do livro.");

                var valid = await ValidarRequest(new LivroValidator(), request);
                if (valid != null)
                    return valid;


                if (await _livroRepository.LivroCadastrado(request.Id))
                {
                    var entity = await _livroRepository.ObterPorId(request.Id);
                    var livro = _mapper.Map<Livro>(request);
                    livro.DataAlteracao = DateTime.Now;
                    livro.DataCadastro = entity.DataCadastro;

                    var response = await ObterStatusCode(
                          "Livro atualizado com sucesso.",
                          StatusCodes.Status200OK,
                          _mapper.Map<ResponseLivro>(await _livroRepository.Atualizar(livro)));

                    _logger.LogInformation("Fim da atualização do livro.");

                    return response;
                }
                else
                {
                    return await ObterStatusCode("Livro informado não localizado.", StatusCodes.Status400BadRequest);
                }

            }
            catch (Exception ex)
            {
                return await ObterStatusCode("Erro ao atualizar o livro", StatusCodes.Status400BadRequest, null, ex);
            }
        }

        public async Task<BaseResponse> ObterLista()
        { 
            try 
            {
                _logger.LogInformation("Inicio busca lista de livros");

                var response =  await ObterStatusCode("Lista de livros carregada com sucesso.", 
                                      StatusCodes.Status200OK,
                                      _mapper.Map<List<ResponseLivro>>(await _livroRepository.ObterLista()));

                _logger.LogInformation("Fim da busca lista de livros");

                return response;
            }
            catch(Exception ex) { 
                return await ObterStatusCode("Erro ao retornar lista de livros",  StatusCodes.Status400BadRequest, null, ex); 
            } 
        }

        public async Task<BaseResponse> ObterPorId(long Id)
        {
            try
            {
                _logger.LogInformation("Inicio busca do livro por Id");

                var livro = _mapper.Map<ResponseLivro>(await _livroRepository.ObterPorId(Id));

                var response = livro != null ?
                                    await ObterStatusCode( "Livro carregado com sucesso.", StatusCodes.Status200OK, livro) :
                                    await ObterStatusCode("Livro informado não localizado.", StatusCodes.Status400BadRequest);

                _logger.LogInformation("Fim da busca do livro por Id");

                return response;
            }
            catch (Exception ex)
            { 
                return await ObterStatusCode("Erro ao retornar o livro", StatusCodes.Status400BadRequest,  null, ex); 
            }

           
        }

    }
}
