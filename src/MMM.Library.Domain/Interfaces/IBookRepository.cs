using MMM.Library.Domain.Core.Data;
using MMM.Library.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Domain.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {

        Task<IEnumerable<Book>> GetBookByCategory(int code);
        Task<IEnumerable<Book>> GetAllBooksWithCategoryAndAuthor();
    }
}
