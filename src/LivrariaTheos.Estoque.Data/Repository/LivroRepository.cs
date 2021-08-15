using LivrariaTheos.Core.Data;
using LivrariaTheos.Estoque.Domain.Livros;
using LivrariaTheos.Estoque.Domain.Livros.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Data.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly EstoqueContext _context;

        public LivroRepository(EstoqueContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Livro> ObterPorId(int id)
        {
            return await _context.Livros.AsNoTracking()
                .Include(a => a.Autor)
                .Include(g => g.Genero)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Livro>> ObterPorNome(string nome)
        {
            return await _context.Livros.AsNoTracking()
                .Where(c => c.Nome.Equals(nome))
                .Include(a => a.Autor)
                .Include(g => g.Genero)
                .ToListAsync();
        }

        public async Task<IEnumerable<Livro>> ObterTodosAtivos()
        {
            return await _context.Livros.AsNoTracking().Where(c => c.Ativo)
                .Include(a => a.Autor)
                .Include(g => g.Genero)
                .ToListAsync();
        }

        public async Task<IEnumerable<Livro>> ObterTodos()
        {
            return await _context.Livros.AsNoTracking()
                .Include(a => a.Autor)
                .Include(g => g.Genero)
                .ToListAsync();
        }

        public async Task<IEnumerable<Livro>> ObterPorAutor(int autorId)
        {
            return await _context.Livros.AsNoTracking()
                .Where(c => c.AutorId == autorId)
                .Include(a => a.Autor)
                .Include(g => g.Genero)
                .ToListAsync();
        }

        public async Task<IEnumerable<Livro>> ObterPorGenero(int generoId)
        {
            return await _context.Livros.AsNoTracking()
                .Where(c => c.GeneroId == generoId)
                .Include(a => a.Autor)
                .Include(g => g.Genero)
                .ToListAsync();
        }

        public async Task<IEnumerable<Livro>> ObterPorNacionalidadeDoAutor(int nacionalidadeAutor)
        {
            return await _context.Livros.AsNoTracking()
                .Where(c => c.Autor.Nacionalidade == nacionalidadeAutor)
                .Include(a => a.Autor)
                .Include(g => g.Genero)
                .ToListAsync();
        }

        public void Adicionar(Livro livro)
        {
            _context.Add(livro);
        }

        public void Atualizar(Livro livro)
        {
            _context.Update(livro);
        }

        public void Excluir(Livro livro)
        {           
            _context.Remove(livro);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }        
    }
}