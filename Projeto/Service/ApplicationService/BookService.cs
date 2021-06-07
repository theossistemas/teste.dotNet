using AutoMapper;
using Data.Repository.Wrapper;
using Domain.Entity;
using Domain.Interface;
using Domain.Model.Book;
using System.Collections.Generic;

namespace Service.ApplicationService
{
    public class BookService : IBookService
    {
        private IRepositoryWrapper _repository { get; } 
        private IMapper _mapper { get; }
        public BookService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repository = repositoryWrapper;
            _mapper = mapper;
        }
        public BookModel AddBook(CreateBookModel book)
        {
            var exists = _repository.Book.TitleExists(book.Title);

            if (exists)
                return default;
            
            var bookEntity = _mapper.Map<Book>(book);

            _repository.Book.Insert(bookEntity);
            _repository.SaveChanges();

            var bookModel = _mapper.Map<BookModel>(bookEntity);

            return bookModel;
        }

        public bool DeleteBook(int id)
        {
            var deleteEntity = _repository.Book.GetBookById(id);

            if (deleteEntity == null)
                return false;

            _repository.Book.Delete(deleteEntity);
            _repository.SaveChanges();

            return true;
        }

        public IEnumerable<BookModel> GetAllBooks()
        {
            var books = _repository.Book.GetAllBooksSortedByName();

            var booksModel = _mapper.Map<IEnumerable<BookModel>>(books);

            return booksModel;

        }

        public UpdateBookModel UpdateBook(UpdateBookModel book, int id)
        {
            var exists = _repository.Book.TitleExists(book.Title);

            if (exists)
                return default;

            var updateEntity = _repository.Book.GetBookById(id);

            if (updateEntity == null)
            {
                book.Title = string.Empty;
                return book;
            }

            _mapper.Map(book, updateEntity);

            _repository.Book.Update(updateEntity);
            _repository.SaveChanges();

            return book;
        }
    }
}
