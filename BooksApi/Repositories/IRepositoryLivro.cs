using System.Collections.Generic;
using System.Threading.Tasks;
using BooksApi.Models;

namespace BooksApi.Repositories
{
    public interface IRepositoryLivro : IRepository
    {
        Task<List<Livro>> getAllBookAsync();
        Task<Livro> getBookByIdAsync(int id);
        
        bool BookExist(string isbn);
    }
}