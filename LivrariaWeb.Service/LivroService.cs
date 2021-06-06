using LivrariaWeb.Domain.Interface;
using LivrariaWeb.Domain.Model;
using LivrariaWeb.Dto;
using LivrariaWeb.Service.Interface;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaWeb.Service
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly ILogger<LivroService> _logger;

        public LivroService(ILivroRepository livroRepository, ILogger<LivroService> logger)
        {
            _livroRepository = livroRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<DtoLivro>> GetAll()
        {
            var dtolivros = await _livroRepository.GetAll();
            return dtolivros.Select(x => new DtoLivro
            {
                NomeLivro = x.NomeLivro,
                NomeAutor = x.NomeAutor,
                DataPublicacao = x.DataPublicacao,
                NumeroPaginas = x.NumeroPaginas,
                Id = x.Id
            }).OrderBy(o => o.NomeLivro);
        }

        public async Task<DtoResult<DtoLivro>> Cadastrar(DtoLivro dtoLivro)
        {
            DtoResult<DtoLivro> dtoResult = new DtoResult<DtoLivro>();

            try
            {
                bool foicadastrado = VerificaSeLivroJaFoiCadastrado(dtoLivro.NomeLivro, dtoLivro.NomeAutor);
                if (!foicadastrado)
                {
                    Livro livro = new Livro(dtoLivro.NomeLivro, dtoLivro.NomeAutor, dtoLivro.DataPublicacao, dtoLivro.NumeroPaginas);

                    _livroRepository.Add(livro);

                    dtoResult.Result = new DtoLivro
                    {
                        Id = livro.Id,
                        NomeLivro = livro.NomeLivro,
                        NomeAutor = livro.NomeAutor,
                        DataPublicacao = livro.DataPublicacao,
                        NumeroPaginas = livro.NumeroPaginas
                    };
                    dtoResult.Message = "Livro cadastrado com sucesso.";
                    return dtoResult;
                }
                else
                    dtoResult.Message = "Livro já consta em sua base de dados.";

                return dtoResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return dtoResult;
            }
        }

        protected bool VerificaSeLivroJaFoiCadastrado(string nomeLivro, string nomeAutor)
        {
            return _livroRepository.VerificaSeLivroJaFoiCadastrado(nomeLivro, nomeAutor);
        }

        public async Task<DtoResult<DtoLivro>> GetLivroById(long idLivro)
        {
            DtoResult<DtoLivro> dtoResult = new DtoResult<DtoLivro>();

            try
            {
                var verificaSeLivroExiste = _livroRepository.VerificaSeLivroExiste(idLivro);

                if (verificaSeLivroExiste)
                {
                    var dtoLivro = await _livroRepository.GetEntityById(idLivro);
                    dtoResult.Result = new DtoLivro
                    {
                        NomeLivro = dtoLivro.NomeLivro,
                        NomeAutor = dtoLivro.NomeAutor,
                        DataPublicacao = dtoLivro.DataPublicacao,
                        NumeroPaginas = dtoLivro.NumeroPaginas,
                        Id = dtoLivro.Id
                    };
                    dtoResult.Message = "Livro Existe.";
                    return dtoResult;
                }
                else
                    dtoResult.Message = "Livro não encontrado.";

                return dtoResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return dtoResult;
            }
        }

        public async Task<DtoResult<DtoLivro>> UpdateLivro(DtoLivro dtoLivro)
        {
            DtoResult<DtoLivro> dtoResult = new DtoResult<DtoLivro>();
            try
            {
                var Update_livro = _livroRepository.GetEntityById(dtoLivro.Id).Result;


                if (Update_livro != null)
                {
                    bool foicadastrado = VerificaSeLivroJaFoiCadastrado(dtoLivro.NomeLivro, dtoLivro.NomeAutor);
                    if (!foicadastrado)
                    {
                        var livro = _livroRepository.GetEntityById(dtoLivro.Id).Result;


                        Update_livro.Id = livro.Id;
                        Update_livro.NomeLivro = dtoLivro.NomeLivro;
                        Update_livro.NomeAutor = dtoLivro.NomeAutor;
                        Update_livro.NumeroPaginas = dtoLivro.NumeroPaginas;
                        Update_livro.DataPublicacao = dtoLivro.DataPublicacao;

                        var update = _livroRepository.Update(Update_livro).Result;
                        dtoResult.Message = "Livro alterado com sucesso.";
                        return dtoResult;
                    }
                    else
                        dtoResult.Message = "Livro já consta em sua base de dados.";

                }
                else
                    dtoResult.Message = "Livro não consta na base de dados.";
            }
            catch (Exception ex)
            {
                dtoResult.Message = "Houve um erro ao editar o Livro.";
                _logger.LogError(ex.Message);
                return dtoResult;
            }
            return dtoResult;
        }

        public async Task<DtoResult<DtoLivro>> DeleteCurso(long idLivro)
        {
            DtoResult<DtoLivro> dtoResult = new DtoResult<DtoLivro>();
            try
            {
                var livro = await _livroRepository.GetEntityById(idLivro);
                if (livro != null)
                {
                    await _livroRepository.Delete(livro);
                    dtoResult.Message = "Livro deletado com sucesso.";
                }
                else
                    dtoResult.Message = "Livro não existe.";

                return dtoResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return dtoResult;
            }


        }
    }
}