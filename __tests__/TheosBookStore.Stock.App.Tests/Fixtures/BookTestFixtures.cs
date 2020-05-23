using System;
using System.Collections.Generic;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.ValueObjects;

namespace TheosBookStore.Stock.App.Tests.Fixtures
{
    public class BookTestFixtures
    {
        public Book GetValidBook()
        {
            return new Book(
                "The book title",
                new ISBN("123456789012"),
                new List<Author>{
                    new Author("Author name")
                },
                1,
                new Publisher("Publisher name"),
                2020,
                1,
                "Rio de Janeiro");
        }

        internal BookRequest GetValidBookInsertRequest()
        {
            return new BookRequest
            {
                Title = "The book title",
                ISBN = "123456789012",
                Authors = new List<AuthorDTO>{
                    new AuthorDTO
                    {
                        Id = 1,
                        Name = "Author name"
                    }
                },
                PageCount = 1,
                Publisher = new PublisherDTO
                {
                    Id = 1,
                    Name = "Publisher name"
                },
                Year = 2020,
                Edition = 1,
                City = "Rio de Janeiro"
            };
        }

        public Book GetInvalidBook()
        {
            return new Book(
                "Ta",
                new ISBN("01234567890123"),
                new List<Author>{
                    new Author(string.Empty)
                },
                1,
                new Publisher(string.Empty),
                2020,
                0,
                "Ri");
        }

        public BookRequest GetInvalidBookRequest()
        {
            return new BookRequest
            {
                Title = "Th",
                ISBN = "12345456a456789012",
                Authors = new List<AuthorDTO>{
                    new AuthorDTO
                    {
                        Id = 1,
                        Name = "Author name"
                    }
                },
                PageCount = 1,
                Publisher = new PublisherDTO
                {
                    Id = 1,
                    Name = "Publisher name"
                },
                Year = 2020,
                Edition = 1,
                City = "Rio de Janeiro"
            };
        }
    }
}
