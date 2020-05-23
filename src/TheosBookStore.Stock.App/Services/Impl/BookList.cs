using System.Collections.Generic;
using System.Linq;
using TheosBookStore.LibCommon.Services;
using TheosBookStore.Stock.App.Factories;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.Domain.Repositories;

namespace TheosBookStore.Stock.App.Services.Impl
{
    public class BookList : ServiceBase, IBookList
    {
        private readonly IBookRepository _repository;
        private readonly IBookFactory _factory;

        public BookList(IBookRepository repository, IBookFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        public ICollection<BookResponse> GetOrderedByTitle(int take, int offset)
        {
            List<BookResponse> responseList = new List<BookResponse>();

            if (take > 50)
            {
                AddErrorMessage("You can not take more than 50 book by request");
                return responseList;
            }

            var bookList = _repository.GetOrderedTitleBookList(take, offset);
            if (bookList.Any())
                responseList.AddRange(
                    bookList
                        .Select(book => _factory.FromEntityToResponse(book))
                        .ToList());
            return responseList;
        }
    }
}
