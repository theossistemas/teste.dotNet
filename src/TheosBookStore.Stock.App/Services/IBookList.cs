using System.Collections.Generic;
using TheosBookStore.LibCommon.Services;
using TheosBookStore.Stock.App.Models;

namespace TheosBookStore.Stock.App.Services
{
    public interface IBookList : IServiceBase
    {
        ICollection<BookResponse> GetOrderedByTitle(int take, int offset);
    }
}
