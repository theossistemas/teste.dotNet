using Livraria.Domain;
using Livraria.Domain.Exceptions;
using Livraria.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Infrastructure.Repositories
{
    public class LivroReadRepository : ILivroReadRepository
    {

        private readonly LivrariaDbContext _context;
        private readonly ILogger<LivroReadRepository> _logger;

        public LivroReadRepository(LivrariaDbContext context, ILogger<LivroReadRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region[GET]

        public async Task<Livro?> GetByIdAsync(int id)
        {
            var livro = await _context.Livros.FindAsync(id);

            if (livro == null)
            {
                _logger.LogWarning($"Livro com id {id} não encontrado ou inativo");
                throw new NaoEncontradoException($"Livro com id {id} não foi encontrado ou está inativo no banco de dados.");
            }
            return livro;
        }
        public async Task<IEnumerable<Livro>> GetAllAsync()
        {
            try
            {
                var livros = await _context.Livros
                    .Where(l => l.Ativo)
                    .OrderBy(l => l.Titulo)
                    .ToListAsync();

                if (livros == null || !livros.Any())
                {
                    _logger.LogWarning("Nenhum livro ativo encontrado.");
                    throw new NaoEncontradoException("Nenhum livro ativo encontrado.");
                }
                return livros;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning($"Erro ao consultar os livros. ex: {ex}");
                throw new ConflitoDeDadosException("Erro ao consultar os livros.");
            }
        }
        public async Task<(IEnumerable<Livro> Livros, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = _context.Livros.Where(l => l.Ativo);
                var totalCount = await query.CountAsync();
                var livros = await query
                    .OrderBy(l => l.Titulo)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                if (livros == null || !livros.Any())
                {
                    _logger.LogWarning("Nenhum livro ativo encontrado para os parâmetros informados.");
                    throw new NaoEncontradoException("Nenhum livro ativo encontrado para os parâmetros informados.");
                }
                return (livros, totalCount);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning($"Erro ao consultar os livros paginados. ex: {ex}");
                throw new ConflitoDeDadosException("Erro ao consultar os livros paginados.");
            }
        }
        #endregion
    }
}
