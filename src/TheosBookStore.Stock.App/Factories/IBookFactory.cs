using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.Domain.Entities;

namespace TheosBookStore.Stock.App.Factories
{
    public interface IBookFactory
    {
        Book FromInsertRequest(BookInsertRequest bookInsert);
    }
}
