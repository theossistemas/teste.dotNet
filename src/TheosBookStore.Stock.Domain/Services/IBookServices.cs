using TheosBookStore.LibCommon.Services;
using TheosBookStore.Stock.Domain.Entities;

namespace TheosBookStore.Stock.Domain.Services
{
    public interface IBookServices : IServiceBase
    {
        void Register(Book book);
    }
}
