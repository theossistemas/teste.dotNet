using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using BooksApi.Data;
 using BooksApi.Models;
 using Microsoft.EntityFrameworkCore;

 namespace BooksApi.Repositories
{
    public class LivroRepository : IRepositoryLivro
    {
        public readonly DataContext _Context;
        
        public LivroRepository(DataContext context)
        {
            _Context = context;
            _Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        public void Add<T>(T entity) where T : class
        {
            _Context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _Context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _Context.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<List<Livro>> getAllBookAsync()
        {
            var books = await _Context.Livros.OrderBy(o=>o.Titulo).ToListAsync();
            return books;
        }

        public async Task<Livro> getBookByIdAsync(int id)
        {
            var books = await _Context.Livros.Where(c => c.Id == id).FirstOrDefaultAsync();
            return books;
        }
        public  bool BookExist(string isbn)
        {
            var reg =  _Context.Livros.FirstOrDefault(x => x.Isbn == isbn);

            return reg != null;

        }

    }
}