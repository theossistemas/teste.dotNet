using System.Collections.Generic;
using FluentAssertions;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.ValueObjects;
using Xunit;

namespace TheosBookStore.Stock.Domain.Tests.Entities
{
    public class BookTest
    {
        const string TITLE = "A Title book";
        const string ISBN = "978011000222";
        const int PAGE_COUNT = 100;
        const string PUBLISHER_COMPANY = "Publisher Company";
        const int YEAR_PUBLICATION = 2020;
        const int EDITION = 3;
        const string CITY = "Rio de Janeiro";

        [Fact]
        public void ShouldCreateABook()
        {
            var authorList = GetAuthorList();
            var expectedISBN = new ISBN(ISBN);

            var book = GetBookPrototype();

            book.Should().NotBeNull();
            book.Title.Should().Be(TITLE);
            book.ISBN.Should().Be(expectedISBN);
            book.Authors.Should().BeEquivalentTo(authorList);
            book.PageCount.Should().Be(PAGE_COUNT);
            book.Publisher.Should().Be(PUBLISHER_COMPANY);
            book.YearPublication.Should().Be(YEAR_PUBLICATION);
            book.Edition.Should().Be(EDITION);
            book.City.Should().Be(CITY);
        }

        [Fact]
        public void ShouldAddAAuthor()
        {
            var newAuthor = new Author("New Author");
            var book = GetBookPrototype();

            book.AddAuthor(newAuthor);

            book.Authors.Should().Contain(newAuthor);
        }

        [Fact]
        public void ShouldBecomeInvalidWhenAnyAuthorIsInvalid()
        {
            var book = GetBookPrototype();
            var newAuthor = new Author(string.Empty);

            book.AddAuthor(newAuthor);

            book.IsValid().Should().BeFalse();
        }

        [Fact]
        public void ShouldBecomeInvalidWhenISBNIsNotValid()
        {
            var book = GetBookPrototype(new ISBN("123"));

            var invalidBook = !book.IsValid();

            invalidBook.Should().BeTrue();
        }

        private Book GetBookPrototype(ISBN isbn = null)
        {
            return new Book(
                "A Title book",
                isbn: isbn ?? new ISBN(ISBN),
                GetAuthorList(),
                pageCount: 100,
                "Publisher Company",
                yearPublication: 2020,
                edition: 3,
                city: "Rio de Janeiro");
        }

        private IList<Author> GetAuthorList()
        {
            return new List<Author>()
            {
                new Author("Author1 name"),
                new Author("Author2 name")
            };
        }
    }
}
