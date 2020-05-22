using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.ValueObjects;
using Xunit;

namespace TheosBookStore.Stock.Domain.Tests.Entities
{
    public class BookTest
    {
        const string TITLE = "The Title Book";
        const string ISBN = "978011000222";
        const string CITY = "Rio de Janeiro";
        const string PUBLISHER_NAME = "Publisher Name";
        const int PAGE_COUNT = 100;
        const int YEAR_PUBLICATION = 2020;
        const int EDITION = 3;


        [Fact]
        public void ShouldCreateABook()
        {
            var authorList = GetAuthorList();
            var expectedISBN = new ISBN(ISBN);
            var expectedPublisher = new Publisher(PUBLISHER_NAME);

            var book = new Book(
                TITLE,
                expectedISBN,
                GetAuthorList(),
                PAGE_COUNT,
                expectedPublisher,
                YEAR_PUBLICATION,
                edition: 3,
                CITY);

            book.Should().NotBeNull();
            book.Title.Should().Be(TITLE);
            book.ISBN.Should().Be(expectedISBN);
            book.Authors.Should().BeEquivalentTo(authorList);
            book.PageCount.Should().Be(PAGE_COUNT);
            book.Publisher.Should().Be(expectedPublisher);
            book.YearPublication.Should().Be(YEAR_PUBLICATION);
            book.Edition.Should().Be(EDITION);
            book.City.Should().Be(CITY);
        }

        [Fact]
        public void ShouldBeInvalidWhenTitleHasLessThen3Char()
        {
            var invalidTitle = "Ab";
            var book = new Book(
                invalidTitle,
                new ISBN(ISBN),
                GetAuthorList(),
                PAGE_COUNT,
                new Publisher(PUBLISHER_NAME),
                YEAR_PUBLICATION,
                edition: 3,
                CITY);

            var valid = book.IsValid();
            var errorMessages = book.ValidationResult.Errors
                .Select(error => error.ErrorMessage);

            valid.Should().BeFalse();
            errorMessages.Should().Contain("Book's title should have 3 characters at least");
        }

        [Fact]
        public void ShouldBeInvalidWhenEditionIsSmallerThanOne()
        {
            var invalidEdition = 0;
            var book = new Book(
                TITLE,
                new ISBN(ISBN),
                GetAuthorList(),
                PAGE_COUNT,
                new Publisher(PUBLISHER_NAME),
                YEAR_PUBLICATION,
                invalidEdition,
                CITY);

            var valid = book.IsValid();
            var errorMessages = book.ValidationResult.Errors
                .Select(error => error.ErrorMessage);

            valid.Should().BeFalse();
            errorMessages.Should().Contain("Book's edition should be greather or equal than 1");
        }

        [Fact]
        public void ShouldBeValidWhenCityNameIsHaveMinimumLenght()
        {
            var invalidCityName = "Jaú";
            var book = new Book(
                TITLE,
                new ISBN(ISBN),
                GetAuthorList(),
                PAGE_COUNT,
                new Publisher(PUBLISHER_NAME),
                YEAR_PUBLICATION,
                EDITION,
                invalidCityName);

            var valid = book.IsValid();
            var errorMessages = book.ValidationResult.Errors
                .Select(error => error.ErrorMessage);

            valid.Should().BeTrue();
        }


        [Fact]
        public void ShouldBeInvalidWhenCityNameIsSmallerThan3Characters()
        {
            var invalidCityName = "Ac";
            var book = new Book(
                TITLE,
                new ISBN(ISBN),
                GetAuthorList(),
                PAGE_COUNT,
                new Publisher(PUBLISHER_NAME),
                YEAR_PUBLICATION,
                EDITION,
                invalidCityName);

            var valid = book.IsValid();
            var errorMessages = book.ValidationResult.Errors
                .Select(error => error.ErrorMessage);

            valid.Should().BeFalse();
            errorMessages.Should().Contain("Book's city is invalid");
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
            var invalidISBN = new ISBN("123");
            var book = new Book(
                TITLE,
                invalidISBN,
                GetAuthorList(),
                PAGE_COUNT,
                new Publisher(PUBLISHER_NAME),
                YEAR_PUBLICATION,
                edition: 3,
                CITY);

            var invalidBook = !book.IsValid();

            invalidBook.Should().BeTrue();
        }

        [Fact]
        public void ShouldBeInvalidWhenPublisherIsInvalid()
        {
            var invalidPublisher = new Publisher(string.Empty);
            var book = new Book(
                TITLE,
                new ISBN(ISBN),
                GetAuthorList(),
                PAGE_COUNT,
                invalidPublisher,
                YEAR_PUBLICATION,
                edition: 3,
                CITY);

            var validBook = book.IsValid();

            validBook.Should().BeFalse();
        }

        private Book GetBookPrototype()
        {
            return new Book(
                TITLE,
                new ISBN(ISBN),
                GetAuthorList(),
                PAGE_COUNT,
                new Publisher(PUBLISHER_NAME),
                YEAR_PUBLICATION,
                edition: 3,
                CITY);
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
