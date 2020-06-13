using MMM.Library.Domain.CQRS.Queries.ViewModels;
using MMM.Library.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Domain.CQRS.Queries
{
    public class BookQueries : IBookQueries
    {
        private readonly IBookRepository _bookRepository;

        public BookQueries(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookAndCategoryViewModel>> GetAllBooksWithCategory()
        {
            var books = await _bookRepository.GetAllBooksWithCategoryAndAuthor();

            var booksViewModel = new List<BookAndCategoryViewModel>();

            foreach (var book in books)
            {
                booksViewModel.Add(new BookAndCategoryViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Year = book.Year,
                    Language = book.Language,
                    Location = book.Location,
                    Category = new CategoryViewModelQueries
                    {
                        Id = book.CategoryId,
                        CategoryName = book.Category.CategoryName,
                        Code = book.Category.Code,
                    }
                });
            }

            return booksViewModel;
        }
    }
}
