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
    public class CategoriaBusiness : ICategoriaBusiness
    {
        private readonly IRepository<CategoriaEntity> _categoriasEntityRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerHelper _loggerHelper;

        public CategoriaBusiness(IRepository<CategoriaEntity> categoriasEntityRepository,
                                 IMapper mapper,
                                 ILoggerHelper loggerHelper)
        {
            _categoriasEntityRepository = categoriasEntityRepository;
            _mapper = mapper;
            _loggerHelper = loggerHelper;
        }
        public async Task<bool> VerificarSeCategoriaJaExiste(string nome)
        {
            return await _categoriasEntityRepository.Exists(x => x.Nome == nome);
        }

        public async Task<CategoriaDTO> CadastrarNovaCategoria(CategoriaDTO categoriaDTO)
        {
            try
            {
                if (await VerificarSeCategoriaJaExiste(categoriaDTO.Nome))
                {
                    await _loggerHelper.RegistrarLog($"A categoria '{categoriaDTO.Nome}' já existe.", "Erro");
                    throw new Exception("Categoria já cadastrada.");
                }

                var novaCategoria = _mapper.Map<CategoriaEntity>(categoriaDTO);

                novaCategoria.Ativo = true;
                novaCategoria.DataDeRegistro = DateTime.Now;

                await _categoriasEntityRepository.InsertAsync(novaCategoria);
                await _loggerHelper.RegistrarLog($"Categoria '{novaCategoria.Nome}' cadastrada com sucesso.", "Success");

                return _mapper.Map<CategoriaDTO>(novaCategoria);
            }
            catch (Exception ex)
            {
                await _loggerHelper.RegistrarLogDeErro(ex);
                throw;
            }    
        }

        public async Task<List<CategoriaDTO>> ListarCategorias()
        {
            var categoriasCadastradas = await _categoriasEntityRepository.GetAllAsync();

            var categoriasFiltradas = categoriasCadastradas.Where(x => x.Ativo == true)
                                                           .OrderBy(x => x.Nome)
                                                           .ToList();

            if (categoriasFiltradas is null)
                throw new Exception("");

            var categoriasFiltradasMapeadas = _mapper.Map<List<CategoriaDTO>>(categoriasFiltradas);

            return categoriasFiltradasMapeadas;
        }

        public string PesquisarCategoriaPeloId(int id)
        {
            var nomeDaCategoria = _categoriasEntityRepository.GetByIdAsync(id).Result.Nome;

            if (nomeDaCategoria is null)
                throw new Exception("Nenhuma categoria encontrada.");

            return nomeDaCategoria;
        }

        public async Task<CategoriaDTO> AtualizarCategoria(CategoriaDTO categoriaDTO)
        {
            try
            {
                var categoria = await _categoriasEntityRepository.GetByIdAsync((int)categoriaDTO.Id);

                if (categoria == null)
                    throw new Exception("Categoria não encontrada.");

                categoria.Nome = categoriaDTO.Nome;

                await _categoriasEntityRepository.UpdateAsync(categoria);
                await _loggerHelper.RegistrarLog($"Categoria '{categoria.Nome}' atualizada com sucesso.", "Success");

                return _mapper.Map<CategoriaDTO>(categoria);
            }
            catch (Exception ex)
            {
                await _loggerHelper.RegistrarLogDeErro(ex);
                throw;
            }        
        }

        public async Task<bool> ExcluirCategoria(int id)
        {
            var categoria = await _categoriasEntityRepository.GetByIdAsync(id);

            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            try
            {
                await _categoriasEntityRepository.DeleteAsync(id);
                await _loggerHelper.RegistrarLog($"Categoria com nome '{categoria.Nome}' e Id '{categoria.Id}' excluída com sucesso.", "Success");

                return true;
            }
            catch (Exception ex)
            {
                await _loggerHelper.RegistrarLogDeErro(ex);
                throw;
            }         
        }
    }
}
