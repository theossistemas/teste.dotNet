using TheosBookStore.LibCommon.Services;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.Domain.Entities;

namespace TheosBookStore.Stock.App.Services
{
    public interface IBookRegister : IServiceBase
    {
        void Register(BookInsertRequest bookInsertRequest);
    }
}
