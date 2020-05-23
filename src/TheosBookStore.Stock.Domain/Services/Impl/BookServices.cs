using System;
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

            try
            {
                _bookRepository.Register(book);
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }
        }

        public Book Remove(int Id)
        {
            var book = _bookRepository.GetById(Id);
            if (book == null)
            {
                AddErrorMessage("There no book registered with this id");
                return Book.BookNull();
            }

            _bookRepository.Remove(book);

            return book;
        }

        public void Update(Book book)
        {
            if (book.Id <= 0)
            {
                AddErrorMessage("There's no record to update");
                return;
            }

            if (!book.IsValid())
            {
                book.ValidationResult.Errors.ToList().ForEach(error =>
                    AddErrorMessage(error.ErrorMessage));
                return;
            }

            var bookExists = _bookRepository.HasAny(book);
            if (!bookExists)
            {
                AddErrorMessage($"The book \"{book.Title}\" does not exists. Try to register ti first");
                return;
            }

            _bookRepository.Update(book);
        }
    }
}
