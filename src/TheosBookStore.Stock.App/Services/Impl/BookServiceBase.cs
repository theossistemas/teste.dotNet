using TheosBookStore.LibCommon.Services;
using TheosBookStore.Stock.App.Factories;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.Domain.Repositories;
using TheosBookStore.Stock.Domain.Services;

namespace TheosBookStore.Stock.App.Services.Impl
{
    public abstract class BookServiceBase : ServiceBase, IBookServiceBase
    {
        protected readonly IBookFactory _factory;
        protected readonly IBookServices _services;
        protected readonly IBookRepository _repository;

        public BookServiceBase(IBookFactory bookFactory, IBookServices bookServices,
            IBookRepository repository)
        {
            _factory = bookFactory;
            _services = bookServices;
            _repository = repository;
        }

        protected BookResponse EmptyResponse()
        {
            return new BookResponse();
        }

        public abstract BookResponse Execute(BookRequest request);
    }
}
