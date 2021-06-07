using Domain.Model.Book;
using System.Collections.Generic;

namespace Domain.Interface
{
    public interface IBookService
    {
        IEnumerable<BookModel> GetAllBooks();
        BookModel AddBook(CreateBookModel book);
        UpdateBookModel UpdateBook(UpdateBookModel book, int id);
        bool DeleteBook(int id);
    }
}
