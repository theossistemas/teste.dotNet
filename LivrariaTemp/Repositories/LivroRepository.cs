using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivrariaTemp.Models;

namespace LivrariaTemp.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly ApplicationDbContext _context;

        public LivroRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public void Add(Livro livro)
        {
            _context.Livros.Add(livro);
            _context.SaveChanges();
        }

        public Livro Find(int id)
        {
            var livro = _context.Livros.FirstOrDefault(m => m.LivroId == id);
            return livro;
        }

        public IEnumerable<Livro> GetAll()
        {
            return _context.Livros.ToList().OrderBy(l => l.Titulo);
        }

        public void Remove(int id)
        {
            var livro = _context.Livros.First(m => m.LivroId == id);
            _context.Livros.Remove(livro);
            _context.SaveChanges();
        }

        public void Update(Livro livro)
        {
            _context.Livros.Update(livro);
            _context.SaveChanges();
        }
    }
}
