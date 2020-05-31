using Livraria.Inferfaces.Repository;
using Livraria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Data.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly LivrariaContext _context;

        public LivroRepository(LivrariaContext context)
        {
            _context = context;
        }

        public Livro AddLivro(Livro livro)
        {
            _context.Livros.Add(livro);
            _context.SaveChanges();
            return livro;
        }

        public IEnumerable<Livro> GetAllLivro()
        {
            return _context.Livros.AsEnumerable().OrderBy(x => x.NomeLivro);
        }

        public Livro GetLivroById(int id)
        {
            return _context.Livros.AsNoTracking()                
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public void RemoveLivro(int id)
        {
            var livro = _context.Livros
                .AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            _context.Livros.Remove(livro);
            _context.SaveChanges();
        }

        public void UpdateLivro(Livro livro)
        {
            _context.Livros.Update(livro);
            _context.SaveChanges();
        }
    }
}
