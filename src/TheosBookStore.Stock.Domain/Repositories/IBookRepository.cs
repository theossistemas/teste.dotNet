using System.Collections.Generic;
using TheosBookStore.LibCommon.Repositories;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.ValueObjects;

namespace TheosBookStore.Stock.Domain.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        bool HasAny(Book book);

        Book GetByISBN(ISBN isbn);

        ICollection<Book> GetOrderedTitleBookList(int take, int offSet);
    }
}
