using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using teste.dotNet.API.Data;
using teste.dotNet.API.DTOs.Request;
using teste.dotNet.API.DTOs.Response;
using teste.dotNet.API.Entities;

namespace teste.dotNet.API.Repository.Impl {
    public class BooksRepositoryImpl : BooksRepository
    {

        private ApplicationDbContext _context;

        public BooksRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }
        public BookResponseDTO Get(int bookId)
        {
            var book =_context.Books
            .Where(book => book.Id == bookId)
            .Select(book => new BookResponseDTO() {
                Id = book.Id,
                ReleaseDate = book.ReleaseDate,
                Title = book.Title,
                WritersName = book.BookWriters.Select(book => book.Writer.Name).ToList()
            }).FirstOrDefault();
            return book;
        }

         public ICollection<BookResponseDTO> List()
        {
            var books = _context.Books
            .OrderBy(book => book.Title)
            .Select(book => new BookResponseDTO() {
                    Id = book.Id,
                    ReleaseDate = book.ReleaseDate,
                    Title = book.Title,
                    WritersName = book.BookWriters.Select(book => book.Writer.Name).ToList()
                }
            ).ToList();            
            
            return books;
        }

        public string Add(BookRequestDTO book)
        {
            var titleExists = _context.Books.Any(book => book.Title.ToLower().Trim() == book.Title.ToLower().Trim());
            if(titleExists) 
                return "Não foi possível cadastrar o livro. Este título já existe na base de dados.";

            var bookEntity = new Book {
                Title = book.Title,
                RegistrationDate = DateTime.Now,
                ReleaseDate = book.ReleaseDate,
                BookWriters = book.WritersId.Select(id => new BookWriter(){WriterId = id}).ToList()
            };

            _context.Add<Book>(bookEntity);
            _context.SaveChanges();
            return "";
        }

        public string Update(int bookId, BookRequestDTO book)
        {
            var titleExists = _context.Books.Any(book => book.Title.ToLower().Trim() == book.Title.ToLower().Trim());
            if(titleExists)
                return "Não foi possível alterar o livro. Este título já existe na base de dados.";

            var bookEntity = _context.Books
            .Include(book => book.BookWriters)
            .Where(book => book.Id == bookId)
            .FirstOrDefault();

            if(bookEntity != null) {
                bookEntity.ReleaseDate = book.ReleaseDate;
                bookEntity.Title = book.Title;
                bookEntity.BookWriters = book.WritersId.Select(writerId => new BookWriter(){BookId= bookId, WriterId = writerId}).ToList();

                _context.Update<Book>(bookEntity);
                _context.SaveChanges();
                return "";
            }
            return "Livro não encontrado na base de dados";
        }

        public string Delete(int bookId)
        {
            var book =_context.Books
            .Include(book => book.BookWriters)
            .Where(book => book.Id == bookId)
            .FirstOrDefault();
            if(book != null) {
                _context.Remove(book);
                _context.SaveChanges();
                return "";
            }
            return "Livro não encontrado na base de dados";
        }
    }
}
