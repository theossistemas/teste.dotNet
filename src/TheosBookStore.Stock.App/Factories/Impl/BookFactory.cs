using System.Collections.Generic;
using System.Linq;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.ValueObjects;

namespace TheosBookStore.Stock.App.Factories.Impl
{
    public class BookFactory : IBookFactory
    {
        public Book FromInsertRequest(BookInsertRequest bookInsert)
        {
            return new Book(
                bookInsert.Title,
                new ISBN(bookInsert.ISBN),
                GetAuthorList(bookInsert.Authors),
                bookInsert.PageCount,
                GetPublisher(bookInsert.Publisher),
                bookInsert.Year,
                bookInsert.Edition,
                bookInsert.City
            );
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
