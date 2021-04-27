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

        public void Add(BookRequestDTO book)
        {
            _bookRepository.Add(book);
        }      

        public void Update(int bookId, BookRequestDTO book)
        {
            throw new System.NotImplementedException();
        }        
        public void Delete(int bookId)
        {
            throw new System.NotImplementedException();
        } 
    }
}
