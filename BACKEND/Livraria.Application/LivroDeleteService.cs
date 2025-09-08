using Livraria.Domain.Exceptions;
using Livraria.Infrastructure.Interfaces;
using Livraria.Service.Interfaces;
using Microsoft.Extensions.Logging;


namespace Livraria.Service
{
    public class LivroDeleteService : ILivroDeleteService
    {
        private readonly ILivroDeleteRepository _livroDeleteRepository;
        private readonly ILogger<LivroDeleteService> _logger;

        public LivroDeleteService(ILivroDeleteRepository livroDeleteRepository, ILogger<LivroDeleteService> logger)
        {
            _livroDeleteRepository = livroDeleteRepository;
            _logger = logger;
        }

        #region[DELETE]
        public async Task<bool> DeleteLivroAsync(int id)
        {
            try
            {
                return await _livroDeleteRepository.DeleteAsync(id);
            }
            catch (NaoEncontradoException ex)
            {
                _logger.LogWarning(ex, $"Livro com id {id} não encontrado para exclusão.");
                return false;
            }
            catch (OperacaoNaoPermitidaException ex)
            {
                _logger.LogWarning(ex, $"Operação não permitida ao tentar excluir livro com id {id}.");
                return false;
            }
            catch (ConflitoDeDadosException ex)
            {
                _logger.LogError(ex, $"Conflito de dados ao tentar excluir livro com id {id}.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao tentar excluir livro com id {id}.");
                throw new ApplicationException("Erro inesperado ao excluir livro.", ex);
            }
        }

        #endregion
    }
}
