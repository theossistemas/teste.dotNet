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
            .Join(_context.Writers, 
                book => book.Id,
                writer => writer.Id,
                (book, writer) => new BookResponseDTO() {
                    Id = book.Id,
                    ReleaseDate = book.ReleaseDate,
                    Title = book.Title,
                    WritersName = book.BookWriters.Select(book => book.Writer.Name).ToList()
                }
            ).ToList();            
            
            return books;
        }

        public void Add(BookRequestDTO book)
        {
            var bookEntity = new Book {
                Title = book.Title,
                RegistrationDate = DateTime.Now,
                ReleaseDate = book.ReleaseDate,
                BookWriters = book.WritersId.Select(id => new BookWriter(){WriterId = id}).ToList()
            };

            _context.Add<Book>(bookEntity);
            _context.SaveChanges();
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
