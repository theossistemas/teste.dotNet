using TheosBookStore.LibCommon.Services;
using TheosBookStore.Stock.App.Models;

namespace TheosBookStore.Stock.App.Services
{
    public interface IBookServiceBase : IServiceBase
    {
        BookResponse Execute(BookRequest request);
    }
}
