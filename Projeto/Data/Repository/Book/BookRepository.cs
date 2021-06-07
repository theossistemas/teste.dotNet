using Data.DatabaseContext;
using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public IEnumerable<Book> GetAllBooksSortedByName()
        {
            return GetAll()
                .OrderBy(b => b.Title)
                .ToList();
        }

        public Book GetBookById(int id)
        {
            return Get(b => b.Id.Equals(id))
                .FirstOrDefault();
        }

        public bool TitleExists(string title)
        {
            return Get(b => b.Title.Equals(title))
                .Any();
        }
    }
}
