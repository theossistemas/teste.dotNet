using System.Linq;
using TheosBookStore.LibCommon.Services;
using TheosBookStore.Stock.App.Factories;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.Services;

namespace TheosBookStore.Stock.App.Services.Impl
{
    public class BookRegister : ServiceBase, IBookRegister
    {
        private readonly IBookFactory _factory;
        private readonly IBookServices _services;

        public BookRegister(IBookFactory bookFactory, IBookServices bookServices)
        {
            _factory = bookFactory;
            _services = bookServices;
        }
        public void Register(BookInsertRequest bookInsertRequest)
        {
            Book book = _factory.FromInsertRequest(bookInsertRequest);
            if (!book.IsValid())
            {
                book.ValidationResult.Errors.ToList().ForEach(error =>
                    AddErrorMessage(error.ErrorMessage));
                return;
            }
            _services.Register(book);
            if (!_services.IsValid)
                AddErrorMessage(_services.GetErrorMessages());
        }
    }
}
