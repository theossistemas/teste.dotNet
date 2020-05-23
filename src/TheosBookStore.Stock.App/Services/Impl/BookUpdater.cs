using System.Linq;
using TheosBookStore.Stock.App.Factories;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.Domain.Repositories;
using TheosBookStore.Stock.Domain.Services;

namespace TheosBookStore.Stock.App.Services.Impl
{
    public class BookUpdater : BookServiceBase, IBookUpdater
    {

        public BookUpdater(IBookFactory bookFactory, IBookServices bookServices,
             IBookRepository repository)
              : base(bookFactory, bookServices, repository) { }

        public override BookResponse Execute(BookRequest request)
        {
            var book = _factory.FromRequest(request);
            if (!book.IsValid())
            {
                book.ValidationResult.Errors.ToList().ForEach(error =>
                    AddErrorMessage(error.ErrorMessage));
                return EmptyResponse();
            }

            _services.Update(book);
            if (!_services.IsValid)
            {
                AddErrorMessage(_services.GetErrorMessages());
                return EmptyResponse();
            }

            var updatedBook = _repository.GetByISBN(book.ISBN);
            return _factory.FromEntityToResponse(updatedBook);
        }
    }
}
