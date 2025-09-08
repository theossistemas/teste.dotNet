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
    public class LivroCreateRepository : ILivroCreateRepository
    {
        private readonly LivrariaDbContext _context;
        private readonly ILogger<LivroCreateRepository> _logger;

        public LivroCreateRepository(LivrariaDbContext context, ILogger<LivroCreateRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region[CREATE]
        public async Task<Livro> AddAsync(Livro livro)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(livro.Titulo) || string.IsNullOrWhiteSpace(livro.Autor))
                {
                    _logger.LogWarning("Título e autor são obrigatórios para criar um novo livro.");
                    throw new OperacaoNaoPermitidaException("Título e autor são obrigatórios.");
                }

                // verificação de ISBN duplicado
                bool isbnDuplicado = await _context.Livros.AnyAsync(l => l.ISBN == livro.ISBN && l.Ativo);
                if (isbnDuplicado)
                {
                    _logger.LogWarning($"Já existe um livro cadastrado com o ISBN {livro.ISBN}.");
                    throw new ConflitoDeDadosException($"Já existe um livro cadastrado com o ISBN {livro.ISBN}.");
                }

                livro.CriadoEm = DateTime.UtcNow;
                _context.Livros.Add(livro);
                await _context.SaveChangesAsync();
                _logger.LogWarning($"Livro com ISBN {livro.ISBN} criado com sucesso.");
                return livro;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning($"Erro ao criar o livro. ex: {ex}");
                throw new ConflitoDeDadosException("Erro ao criar o livro.");
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogWarning($"Contexto já foi descartado. ex: {ex}");
                throw new ConflitoDeDadosException("Erro ao acessar o banco de dados.");
            }
        }
        #endregion
    }
}
