using System.Collections.Generic;
using System.Linq;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.ValueObjects;

namespace TheosBookStore.Stock.App.Factories.Impl
{
    public class BookFactory : IBookFactory
    {
        public BookResponse FromEntityToResponse(Book book)
        {
            if (book == null)
                return null;
            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                Authors = book.Authors.Select(author => new AuthorDTO
                {
                    Id = author.Id,
                    Name = author.Name
                }).ToList(),
                PageCount = book.PageCount,
                Publisher = book.Publisher,
                Year = book.YearPublication,
                Edition = book.Edition,
                City = book.City,
            };
        }

        public Book FromRequest(BookRequest request)
        {
            var book = new Book(
                request.Title,
                new ISBN(request.ISBN),
                GetAuthorList(request.Authors),
                request.PageCount,
                GetPublisher(request.Publisher),
                request.Year,
                request.Edition,
                request.City
            );
            if (request.Id > 0)
                book.DefineId(request.Id);
            return book;
        }

        private List<Author> GetAuthorList(List<AuthorDTO> authors) => authors.ToList().ConvertAll(author =>
        {
            var authorEntity = new Author(author.Name);
            authorEntity.DefineId(author.Id);
            return authorEntity;
        });

        private Publisher GetPublisher(PublisherDTO publisherDTO)
        {
            var newPublisher = new Publisher(publisherDTO.Name);
            newPublisher.DefineId(publisherDTO.Id);
            return newPublisher;
        }
    }
}
