using System.Linq;
using FluentAssertions;
using TheosBookStore.Stock.App.Factories;
using TheosBookStore.Stock.App.Factories.Impl;
using TheosBookStore.Stock.App.Tests.Fixtures;
using TheosBookStore.Stock.Domain.Entities;
using Xunit;

namespace TheosBookStore.Stock.App.Tests.Factories
{
    public class BookFactoryTest : IClassFixture<BookTestFixtures>
    {
        private readonly BookTestFixtures _bookFixture;

        public BookFactoryTest(BookTestFixtures bookFixtures)
        {
            _bookFixture = bookFixtures;
        }

        [Fact]
        public void ShouldCreateBookFromValidBookInsertRequest()
        {
            var bookInsert = _bookFixture.GetValidBookInsertRequest();
            IBookFactory bookFactory = new BookFactory();

            Book book = bookFactory.FromRequest(bookInsert);

            book.Should().NotBeNull();
            book.Title.Should().Be(bookInsert.Title);
            book.ISBN.Value.Should().Be(bookInsert.ISBN);
            book.PageCount.Should().Be(bookInsert.PageCount);
            book.Publisher.Id.Should().Be(bookInsert.Publisher.Id);
            book.Publisher.Name.Should().Be(bookInsert.Publisher.Name);
            book.YearPublication.Should().Be(bookInsert.Year);
            book.Edition.Should().Be(bookInsert.Edition);
            book.City.Should().Be(bookInsert.City);
        }
    }
}
