using Livraria.Domain;
using Livraria.Domain.Exceptions;
using Livraria.DTOs;
using Livraria.Infrastructure.Interfaces;
using Livraria.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Livraria.Service
{
    public class LivroCreateService : ILivroCreateService
    {
        private readonly ILivroCreateRepository _livroCreateRepository;
        private readonly ILogger<LivroCreateService> _logger;

        public LivroCreateService(ILivroCreateRepository livroCreateRepository, ILogger<LivroCreateService> logger)
        {
            _livroCreateRepository = livroCreateRepository;
            _logger = logger;
        }

        #region[CREATE]
        public async Task<LivroDTO?> AddLivroAsync(LivroDTO livroDto)
        {
            try
            {
                var livro = new Livro(
                    0,
                    livroDto.Titulo,
                    livroDto.Autor,
                    livroDto.ISBN,
                    livroDto.AnoPublicacao,
                    livroDto.Quantidade,
                    livroDto.Preco
                )
                {
                    CriadoEm = DateTime.UtcNow,
                    Ativo = true
                };

                var livroCriado = await _livroCreateRepository.AddAsync(livro);
                return new LivroDTO(livroCriado);
            }
            catch (OperacaoNaoPermitidaException ex)
            {
                _logger.LogWarning(ex, "Operação não permitida ao tentar criar livro.");
                return null;
            }
            catch (ConflitoDeDadosException ex)
            {
                _logger.LogError(ex, $"Conflito de dados ao tentar criar livro com ISBN {livroDto.ISBN}.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar criar livro.");
                throw new ApplicationException("Erro inesperado ao criar livro.", ex);
            }
        }
        #endregion
    }
}
