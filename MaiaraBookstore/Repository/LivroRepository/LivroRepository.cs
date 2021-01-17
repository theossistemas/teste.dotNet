using MaiaraBookstore.Data;
using MaiaraBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return _dataContext.Livro.Where(e => e.Id == id).FirstOrDefault();
        }

        public Livro FindByTitulo(string titulo)
        {
            return _dataContext.Livro.Where(e => e.Titulo == titulo).FirstOrDefault();
        }

        public void Save(Livro objeto)
        {
            _dataContext.Livro.Add(objeto);
        }
    }
}
