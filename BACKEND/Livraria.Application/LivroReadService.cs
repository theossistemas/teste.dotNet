using Livraria.Domain.Exceptions;
using Livraria.DTOs;
using Livraria.Infrastructure.Interfaces;
using Livraria.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Livraria.Service
{
    public class LivroReadService : ILivroReadService
    {

        private readonly ILivroReadRepository _livroReadRepository;
        private readonly ILogger<LivroReadService> _logger;

        public LivroReadService(ILivroReadRepository livroReadRepository, ILogger<LivroReadService> logger)
        {
            _livroReadRepository = livroReadRepository;
            _logger = logger;
        }



        #region[GET]

        public async Task<LivroDTO?> GetLivroByIdAsync(int id)
        {
            try
            {
                var livro = await _livroReadRepository.GetByIdAsync(id);
                if (livro == null || !livro.Ativo)
                {
                    _logger.LogWarning($"Livro com id {id} não encontrado ou está inativo.");
                    return null;
                }
                return new LivroDTO(livro);
            }
            catch (NaoEncontradoException ex)
            {
                _logger.LogWarning(ex, $"Livro com id {id} não encontrado.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao buscar livro com id {id}.");
                throw new ApplicationException("Erro inesperado ao buscar livro.", ex);
            }
        }

        public async Task<IEnumerable<LivroDTO?>> GetLivrosAsync()
        {
            try
            {
                var livros = await _livroReadRepository.GetAllAsync();
                var a = livros.Where(l => l.Ativo).ToList().Select(l => new LivroDTO(l));
                return a;
            }
            catch (NaoEncontradoException ex)
            {
                _logger.LogWarning(ex, "Nenhum livro ativo encontrado.");
                return Enumerable.Empty<LivroDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao buscar livros.");
                throw new ApplicationException("Erro inesperado ao buscar livros.", ex);
            }
        }

        public async Task<(IEnumerable<LivroDTO?> Livros, int TotalCount)> GetLivrosPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (livros, totalCount) = await _livroReadRepository.GetPagedAsync(pageNumber, pageSize);
                var livrosDto = livros.Select(l => new LivroDTO(l));
                return (livrosDto, totalCount);
            }
            catch (NaoEncontradoException ex)
            {
                _logger.LogWarning(ex, "Nenhum livro ativo encontrado para os parâmetros informados.");
                return (Enumerable.Empty<LivroDTO>(), 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao buscar livros paginados.");
                throw new ApplicationException("Erro inesperado ao buscar livros paginados.", ex);
            }
        }

        #endregion
    }
}
