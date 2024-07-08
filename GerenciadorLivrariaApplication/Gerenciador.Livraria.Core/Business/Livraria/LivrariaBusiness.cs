using AutoMapper;
using Gerenciador.Livraria.Core.Entities.Livraria;
using Gerenciador.Livraria.Core.Interfaces.BusinessInterface;
using Gerenciador.Livraria.Core.Interfaces.Logs;
using Gerenciador.Livraria.Core.Interfaces.Repositories;
using Gerenciador.Livraria.DTOs.DTOs.Livros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.Core.Business.Livraria
{
    public class LivrariaBusiness : ILivrariaBusiness
    {
        #region Constantes
        private string MENSAGEM_OBRA_NAO_ENCONTRADA = "Volume não encontrado no sistema.";
        private string MENSAGEM_OBRAS_NAO_ENCONTRADAS = "Nenhum volume encontrado no sistema.";
        private string MENSAGEM_AUTOR_NAO_ENCONTRADO = "Autor não encontrado no sistema.";
        private string MENSAGEM_CATEGORIA_NAO_ENCONTRADA = "Categoria não encontrada no sistema.";
        #endregion

        private readonly IRepository<LivroEntity> _livroEntityRepository;
        private readonly IRepository<AutorEntity> _autorEntityRepository;
        private readonly IRepository<CategoriaEntity> _categoriaEntityRepository;
        private readonly ILivroRepository _livroRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerHelper _loggerHelper;

        public LivrariaBusiness(IRepository<LivroEntity> livroEntityRepository, 
                                IRepository<AutorEntity> autorEntityRepository, 
                                IRepository<CategoriaEntity> categoriaEntityRepository,
                                ILivroRepository livroRepository,
                                IMapper mapper,
                                ILoggerHelper loggerHelper)
        {
            _livroEntityRepository = livroEntityRepository;
            _autorEntityRepository = autorEntityRepository;
            _categoriaEntityRepository = categoriaEntityRepository;
            _livroRepository = livroRepository;
            _mapper = mapper;
            _loggerHelper = loggerHelper;
        }

        public async Task<bool> VerificarSeObraJaExiste(string nome)
        {
            return await _livroEntityRepository.Exists(x => x.Nome == nome);
        }

        public async Task<List<LivroDTO>> ListarObras()
        {
            var todasAsObrasCadastradas = await _livroRepository.GetAllIncluded();

            var obrasFiltradas = todasAsObrasCadastradas.Where(x => x.Ativo == true)
                                                        .OrderBy(x => x.Nome)
                                                        .ToList();

            if (obrasFiltradas is null || !obrasFiltradas.Any())
                throw new Exception(MENSAGEM_OBRAS_NAO_ENCONTRADAS);

            var obrasCadastradasMapeadas = _mapper.Map<List<LivroDTO>>(obrasFiltradas);

            return obrasCadastradasMapeadas;
        }

        public async Task CadastrarObra(LivroDTO livroDTO)
        {
            var autor = await _autorEntityRepository.GetByIdAsync(livroDTO.AutorId);

            if (autor is null)
                throw new Exception(MENSAGEM_AUTOR_NAO_ENCONTRADO);

            var categoria = await _categoriaEntityRepository.GetByIdAsync(livroDTO.CategoriaId);
            
            if (categoria is null)
                throw new Exception(MENSAGEM_CATEGORIA_NAO_ENCONTRADA);

            try
            {
                if (await VerificarSeObraJaExiste(livroDTO.Nome))
                {
                    await _loggerHelper.RegistrarLog($"Já existe uma obra com este nome: '{livroDTO.Nome}'", "Erro");
                    throw new Exception("Obra já cadastrada.");
                }

                var livro = new LivroEntity()
                {
                    Nome = livroDTO.Nome,
                    AutorId = livroDTO.AutorId,
                    CategoriaId = livroDTO.CategoriaId,
                    DataDePublicacao = livroDTO.DataDePublicacao,
                    Descricao = livroDTO.Descricao,
                    Ebook = livroDTO.Ebook,
                    DataDeRegistro = DateTime.Now,
                    Ativo = true
                };

                await _livroEntityRepository.InsertAsync(livro);
                await _loggerHelper.RegistrarLog($"Livro '{livro.Nome}' cadastrado com sucesso.", "Success");
            }
            catch (Exception ex)
            {
                await _loggerHelper.RegistrarLogDeErro(ex);
                throw;
            }
        }

        public async Task<bool> ExcluirObra(int id)
        {
            var obra = await _livroEntityRepository.GetByIdAsync(id);
            if (obra is null)
                throw new Exception(MENSAGEM_OBRA_NAO_ENCONTRADA);

            try
            {
                await _livroEntityRepository.DeleteAsync(id);
                await _loggerHelper.RegistrarLog($"Livro com o nome '{obra.Nome}' e Id '{id}' excluído com sucesso.", "Success");

                return true;
            }
            catch (Exception ex)
            {
                await _loggerHelper.RegistrarLogDeErro(ex);
                throw;
            }
        }

        public async Task AtualizarObra(LivroDTO livroDTO)
        {
            if(livroDTO.Id is null)
                throw new ArgumentNullException(nameof(livroDTO), "É necessário informar um livro (Id) válido para realizar essa operação");

            var obra = await _livroEntityRepository.GetByIdAsync((int)livroDTO.Id);

            if (obra is null)
                throw new Exception(MENSAGEM_OBRA_NAO_ENCONTRADA);

            try
            {
                obra.Nome = !string.IsNullOrEmpty(livroDTO.Nome) ? livroDTO.Nome : obra.Nome;
                obra.Descricao = !string.IsNullOrEmpty(livroDTO.Descricao) ? livroDTO.Descricao : obra.Descricao;
                obra.AutorId = livroDTO.AutorId == 0 ? obra.AutorId : livroDTO.AutorId;
                obra.CategoriaId = livroDTO.CategoriaId == 0 ? obra.CategoriaId : livroDTO.CategoriaId;
                obra.DataDePublicacao = livroDTO.DataDePublicacao ?? obra.DataDePublicacao;
                obra.Ebook = livroDTO.Ebook != obra.Ebook ? livroDTO.Ebook : obra.Ebook;

                await _livroEntityRepository.UpdateAsync(obra);
                await _loggerHelper.RegistrarLog($"Livro '{obra.Nome}' atualizado com sucesso.", "Success");
            }
            catch (Exception ex)
            {
                await _loggerHelper.RegistrarLogDeErro(ex);
                throw;
            }    
        }
    }
}
