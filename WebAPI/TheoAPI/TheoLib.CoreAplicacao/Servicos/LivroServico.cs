using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheoLib.CoreAplicacao.Validadores;
using TheoLib.Dominio.Contratos.Repositorio;
using TheoLib.Dominio.Contratos.Servicos;
using TheoLib.Dominio.Entidade;
using TheoLib.Dominio.Modelo;
using TheoLib.Dominio.Modelo.LivroModelo;

namespace TheoLib.CoreAplicacao.Servicos
{
    public class LivroServico : ServicoBase, ILivroServico
    {
        protected readonly ILivroRepositorio _livroRepositorio;

        public LivroServico(ILivroRepositorio livroRepositorio,IMapper mapper, ILogger<LivroServico> logger, IConfiguration configuration) :base (mapper, logger, configuration)
        {
            _livroRepositorio = livroRepositorio;

        }
        public async Task<RespostaBase> Atualizar(RequisicaoLivro model)
        {
            try
            {
                _logger.LogInformation("Iniciando UPDATE do LIVRO.");

                var valido = await ValidarRequest(new LivroValidador(), model);
                if (valido != null)
                    return valido;


                if (await _livroRepositorio.LivroPossuiCadastro(model.Id))
                {
                    var entity = await _livroRepositorio.ObterPorId(model.Id);
                    var livro = _mapper.Map<Livro>(model);
                    var response = await ObterCodigoDoStatus("Livro atualizado com sucesso.", StatusCodes.Status200OK, _mapper.Map<RespostaLivro>(await _livroRepositorio.Atualizar(livro)));
                    _logger.LogInformation("Fim UPDATE do LIVRO.");

                    return response;
                }
                else
                {
                    return await ObterCodigoDoStatus("Livro informado encontrado.", StatusCodes.Status400BadRequest);
                }

            }
            catch (Exception ex)
            {
                return await ObterCodigoDoStatus("Erro ao fazer UPDATE no livro", StatusCodes.Status400BadRequest, null, ex);
            }
        }

        public async Task<RespostaBase> Inserir(RequisicaoLivro model)
        {
            try
            {
                _logger.LogInformation("Iniciando o INSERT do LIVRO.");

                var valido = await ValidarRequest(new LivroValidador(), model);
                if (valido != null)
                    return valido;

                var entity = _mapper.Map<Livro>(model);
                var response = await ObterCodigoDoStatus("Livro cadastrado com sucesso.", StatusCodes.Status201Created, _mapper.Map<RespostaLivro>(await _livroRepositorio.Inserir(entity)));
                
                _logger.LogInformation("Fim do INSERT do livro.");

                return response;
            }
            catch (Exception ex)
            {
                return await ObterCodigoDoStatus("Erro ao Inserir o livro", StatusCodes.Status400BadRequest, null, ex);
            }
        }

        public async Task<RespostaBase> ObterLista()
        {
            try 
            {
                _logger.LogInformation("Iniciando a consulta da lista de livros");

                var response =  await ObterCodigoDoStatus("Lista de livros carregada com sucesso.", StatusCodes.Status200OK,_mapper.Map<List<RespostaLivro>>(await _livroRepositorio.ObterLista()));

                _logger.LogInformation("Fim da busca lista de livros");

                return response;
            }
            catch(Exception ex) { 
                return await ObterCodigoDoStatus("Erro ao consultar a lista de livros",  StatusCodes.Status400BadRequest, null, ex); 
            }
        }

        public async Task<RespostaBase> ObterPorId(long Id)
        {
            try
            {
                _logger.LogInformation("Inicianado a busca por ID {Chave primária} do livro");

                var livro = _mapper.Map<RespostaLivro>(await _livroRepositorio.ObterPorId(Id));

                var response = livro != null ?
                    await ObterCodigoDoStatus( "Consulta OK.", StatusCodes.Status200OK, livro) :
                    await ObterCodigoDoStatus("Registro não localizado.", StatusCodes.Status400BadRequest);

                _logger.LogInformation("Fim da busca por ID {Chave primária} do livro");

                return response;
            }
            catch (Exception ex)
            { 
                return await ObterCodigoDoStatus("Erro ao consultar o livro por ID", StatusCodes.Status400BadRequest,  null, ex); 
            }
        }
    }
}
