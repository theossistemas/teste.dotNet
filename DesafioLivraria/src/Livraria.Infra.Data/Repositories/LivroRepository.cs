using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Repositories
{
    public class LivroRepository : ILivro
    {
        private readonly LivrariaDbContext _context;

        public LivroRepository(LivrariaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Livro>> GetAllAsync()
        {
            return await _context.Livros.ToListAsync();
        }

        public async Task<Livro?> GetByIdAsync(int id)
        {
            return await _context.Livros.FindAsync(id);
        }

        public async Task<Livro?> GetByIsbnAsync(string isbn)
        {
            return await _context.Livros.FirstOrDefaultAsync(x => x.Isbn == isbn);
        }

        public async Task AddAsync(Livro livro)
        {
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Livro livro)
        {
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro != null)
            {
                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();
            }
        }
    }
}
