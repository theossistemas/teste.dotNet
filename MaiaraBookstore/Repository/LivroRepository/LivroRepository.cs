using MaiaraBookstore.Data;
using MaiaraBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiaraBookstore.Repository.LivroRepository
{
    public class LivroRepository : IRepository<Livro>
    {
        private DataContext _dataContext;
        public LivroRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Livro FindById(int id)
        {
            return _dataContext.Livro.SingleOrDefault(l => l.Id == id);
        }

        public Livro FindByTitulo(String titulo)
        {
            return _dataContext.Livro.SingleOrDefault(l => l.Titulo == titulo);
        }

        public List<Livro> FindAll()
        {
            return _dataContext.Livro.OrderBy(l => l.Titulo).ToList();
        }

        public void Save(Livro objeto)
        {
            _dataContext.Livro.Add(objeto);
        }

        public void Delete(Livro livro)
        {
            _dataContext.Livro.Remove(livro);
        }

        public void UpDate(Livro livro)
        {
            _dataContext.Livro.Update(livro);
        }
    }
}
