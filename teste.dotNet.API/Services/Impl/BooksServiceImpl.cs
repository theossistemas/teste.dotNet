using System.Collections.Generic;
using teste.dotNet.API.DTOs.Request;
using teste.dotNet.API.DTOs.Response;
using teste.dotNet.API.Repository;

namespace teste.dotNet.API.Services.Impl {
    public class BooksServiceImpl : BooksService
    {
        private BooksRepository _bookRepository;

        public BooksServiceImpl(BooksRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public BookResponseDTO Get(int bookId)
        {
            var book = _bookRepository.Get(bookId);
            return book;
        }        
        public ICollection<BookResponseDTO> List()
        {
            var books = _bookRepository.List();
            return books;
        }

        public string Add(BookRequestDTO book)
        {
            var message =_bookRepository.Add(book);
            return message;
        }      

        public string Update(int bookId, BookRequestDTO book)
        {
            var message =_bookRepository.Update(bookId, book);
            return message;
        }        
        public string Delete(int bookId)
        {
            var message =_bookRepository.Delete(bookId);
            return message;
        } 
    }
}
