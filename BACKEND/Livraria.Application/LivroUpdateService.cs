using Livraria.Domain.Exceptions;
using Livraria.DTOs;
using Livraria.Infrastructure.Interfaces;
using Livraria.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Livraria.Service
{
    public class LivroUpdateService : ILivroUpdateService
    {
        private readonly ILivroUpdateRepository _livroUpdateRepository;
        private readonly ILivroReadRepository _livroReadRepository;
        private readonly ILogger<LivroUpdateService> _logger;

        public LivroUpdateService(ILivroUpdateRepository livroUpdateRepository, ILivroReadRepository livroReadRepository, ILogger<LivroUpdateService> logger)
        {
            _livroUpdateRepository = livroUpdateRepository;
            _livroReadRepository = livroReadRepository;
            _logger = logger;
        }

        public async Task<LivroDTO?> UpdateLivroAsync(int id, LivroDTO livroDto)
        {
            try
            {
                var livro = await _livroReadRepository.GetByIdAsync(id);
                if (livro == null || !livro.Ativo)
                {
                    _logger.LogWarning($"Tentativa de atualizar livro com id {id} falhou. O livro não foi encontrado ou está inativo.");
                    return null;
                }

                livro.Titulo = livroDto.Titulo;
                livro.Autor = livroDto.Autor;
                livro.ISBN = livroDto.ISBN;
                livro.AnoPublicacao = livroDto.AnoPublicacao;
                livro.Quantidade = livroDto.Quantidade;
                livro.Preco = livroDto.Preco;
                livro.AtualizadoEm = DateTime.UtcNow;

                var livroAtualizado = await _livroUpdateRepository.UpdateAsync(livro);
                return new LivroDTO(livroAtualizado);
            }
            catch (NaoEncontradoException ex)
            {
                _logger.LogWarning(ex, $"Livro com id {id} não encontrado para atualização.");
                return null;
            }
            catch (OperacaoNaoPermitidaException ex)
            {
                _logger.LogWarning(ex, $"Operação não permitida ao tentar atualizar livro com id {id}.");
                return null;
            }
            catch (ConflitoDeDadosException ex)
            {
                _logger.LogError(ex, $"Conflito de dados ao tentar atualizar livro com id {id}.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao tentar atualizar livro com id {id}.");
                throw new ApplicationException("Erro inesperado ao atualizar livro.", ex);
            }
        }
    }
}
