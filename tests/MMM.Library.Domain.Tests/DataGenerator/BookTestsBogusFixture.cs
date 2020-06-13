using Bogus;
using MMM.Library.Domain.Models;
using System;
using Xunit;

namespace MMM.Library.Domain.Tests.DataGenerator
{
    [CollectionDefinition(nameof(BookBogusCollection))]
    public class BookBogusCollection : ICollectionFixture<BookTestsBogusFixture>
    { }
    public class BookTestsBogusFixture
    {
        public static Book GetValidBook()
        {
            var book = new Faker<Book>().CustomInstantiator(f => new Book(
                Guid.NewGuid(),
                Guid.NewGuid(),
                f.Commerce.Department(),
                f.Random.Int(1950, DateTime.Now.Year),
                f.Address.Country(),
                f.Commerce.Locale));


            return book;
        }

        public static Book GetInValidBook()
        {
            var book = new Faker<Book>().CustomInstantiator(f => new Book(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "",
                f.Random.Int(1250, 1500),
                "",
                ""));

            return book;
        }

    }
}
