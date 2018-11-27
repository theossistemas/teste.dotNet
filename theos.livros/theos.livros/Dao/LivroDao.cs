using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using theos.livros.Entitys;
using theos.livros.Models;

namespace theos.livros.Dao
{
    public class LivroDao
    {
        public List<Livro> Listar()
        {
            using (var _context = new LivroContext())
            {
                var _livros = _context.Livros.FromSql("select * from dbo.livros order by titulo").ToList();

                return _livros;
            };
        }

        public Livro Consultar(int id)
        {
            using (var _context = new LivroContext())
            {
                return _context.Livros.Find(id);
            };
        }

        public void InserirLivro(Livro livro)
        {
            using (var _context = new LivroContext())
            {
                _context.Livros.Add(livro);
                _context.SaveChanges();
            }
        }

        public void AtualizarLivro(Livro livro)
        {
            using (var _context = new LivroContext())
            {
                var _livro = _context.Livros.Find(livro.IdLivro);

                _livro.Titulo = livro.Titulo;
                _livro.Autor = livro.Autor;

                _context.Livros.Update(_livro);
                _context.SaveChanges();
            }
        }

        public void RemoverLivro(int id)
        {
            using (var _context = new LivroContext())
            {
                var _livro = _context.Livros.Find(id);

                _context.Livros.Remove(_livro);
                _context.SaveChanges();
            }
        }
    }
}
