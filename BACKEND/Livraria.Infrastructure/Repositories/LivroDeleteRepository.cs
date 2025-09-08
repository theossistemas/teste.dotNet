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
    public class LivroDeleteRepository : ILivroDeleteRepository
    {
        private readonly LivrariaDbContext _context;
        private readonly ILogger<LivroDeleteRepository> _logger;

        public LivroDeleteRepository(LivrariaDbContext context, ILogger<LivroDeleteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var livro = await _context.Livros.FindAsync(id);
                if (livro == null)
                {
                    _logger.LogWarning($"Tentativa de deletar livro com id {id} falhou. O livro não foi encontrado no banco de dados.");
                    throw new NaoEncontradoException($"Livro com id {id} não foi encontrado no banco de dados.");
                }
                if (!livro.Ativo)
                {
                    _logger.LogWarning($"Tentativa de deletar livro com id {id} falhou. O livro já está inativo ou foi excluído.");
                    throw new OperacaoNaoPermitidaException("O livro já está inativo ou foi excluído.");
                }

                livro.Ativo = false;
                livro.ExcluidoEm = DateTime.UtcNow;
                _context.Livros.Update(livro);
                await _context.SaveChangesAsync();
                _logger.LogWarning($"Livro com id {id} deletado com sucesso");
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Erro ao excluir o livro com id {id}. ex: {ex}");
                throw new ConflitoDeDadosException("Erro ao excluir o livro.");
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError($"Contexto já foi descartado ao excluir o livro com id {id}. ex: {ex}");
                throw new ConflitoDeDadosException("Erro ao acessar o banco de dados.");
            }
        }
    }
}
