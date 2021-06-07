using Domain.Entity;
using System.Collections.Generic;

namespace Data.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetAllBooksSortedByName();
        Book GetBookById(int id);
        bool TitleExists(string title);
    }
}
