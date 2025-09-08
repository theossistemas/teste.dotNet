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
    public class LivroUpdateRepository : ILivroUpdateRepository
    {

        private readonly LivrariaDbContext _context;
        private readonly ILogger<LivroUpdateRepository> _logger;
        public LivroUpdateRepository(LivrariaDbContext context, ILogger<LivroUpdateRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Livro> UpdateAsync(Livro livro)
        {
            try
            {
                var livroDb = await _context.Livros.FindAsync(livro.Id);
                if (livroDb == null)
                {
                    throw new NaoEncontradoException($"Livro com id {livro.Id} não encontrado.");
                }
                if (!livroDb.Ativo)
                {
                    throw new OperacaoNaoPermitidaException("O livro está inativo e não pode ser atualizado.");
                }

                // Atualiza os campos necessários
                livroDb.Titulo = livro.Titulo;
                livroDb.Autor = livro.Autor;
                livroDb.ISBN = livro.ISBN;
                livroDb.AnoPublicacao = livro.AnoPublicacao;
                livroDb.Quantidade = livro.Quantidade;
                livroDb.Preco = livro.Preco;
                livroDb.AtualizadoEm = DateTime.UtcNow;

                _context.Livros.Update(livroDb);
                await _context.SaveChangesAsync();
                return livroDb;
            }
            catch (DbUpdateException ex)
            {
                throw new ConflitoDeDadosException("O item não pôde ser atualizado.");
            }
            catch (ObjectDisposedException ex)
            {
                throw new ConflitoDeDadosException("Erro ao acessar o banco de dados.");
            }
        }
    }
}
