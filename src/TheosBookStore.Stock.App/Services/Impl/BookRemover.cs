using TheosBookStore.Stock.App.Factories;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.Domain.Repositories;
using TheosBookStore.Stock.Domain.Services;

namespace TheosBookStore.Stock.App.Services.Impl
{
    public class BookRemover : BookServiceBase, IBookRemover
    {
        public BookRemover(IBookFactory bookFactory, IBookServices bookServices,
            IBookRepository repository) : base(bookFactory, bookServices, repository)
        { }

        public override BookResponse Execute(BookRequest request)
        {
            if (request.Id <= 0)
            {
                AddErrorMessage("There is no book to remove");
                return EmptyResponse();
            }

            var removedBook = _services.Remove(request.Id);
            if (!_services.IsValid)
            {
                AddErrorMessage(_services.GetErrorMessages());
                return EmptyResponse();
            }

            return _factory.FromEntityToResponse(removedBook);
        }
    }
}
