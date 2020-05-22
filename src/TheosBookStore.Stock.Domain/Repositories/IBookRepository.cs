using TheosBookStore.LibCommon.Repositories;
using TheosBookStore.Stock.Domain.Entities;

namespace TheosBookStore.Stock.Domain.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        bool HasAny(Book book);
    }
}
