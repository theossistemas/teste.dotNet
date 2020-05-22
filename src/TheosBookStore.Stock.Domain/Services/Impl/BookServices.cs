using System.Linq;

using TheosBookStore.LibCommon.Services;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.Repositories;

namespace TheosBookStore.Stock.Domain.Services.Impl
{
    public class BookServices : ServiceBase, IBookServices
    {
        private readonly IBookRepository _bookRepository;

        public BookServices(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void Register(Book book)
        {
            if (!book.IsValid())
            {
                book.ValidationResult.Errors.ToList().ForEach(error =>
                    AddErrorMessage(error.ErrorMessage));
                return;
            }

            var alreadyRegistered = _bookRepository.HasAny(book);
            if (alreadyRegistered)
            {
                AddErrorMessage($"The Book \"{book.Title}\" is already registered");
                return;
            }

            _bookRepository.Register(book);
        }
    }
}
