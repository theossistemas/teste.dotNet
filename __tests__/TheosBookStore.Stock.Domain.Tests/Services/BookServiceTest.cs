using System.Collections.Generic;

using FluentAssertions;

using Moq;

using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.Repositories;
using TheosBookStore.Stock.Domain.Services;
using TheosBookStore.Stock.Domain.Services.Impl;
using TheosBookStore.Stock.Domain.ValueObjects;

using Xunit;

namespace TheosBookStore.Stock.Domain.Tests.Services
{

    public class BookServiceTest
    {
        [Fact]
        public void ShouldCreate()
        {
            var bookReposiory = new Mock<IBookRepository>();
            IBookServices bookServices = new BookServices(bookReposiory.Object);

            bookServices.Should().NotBeNull();
        }

        [Fact]
        public void ShouldAddAValidBook()
        {
            var book = GetValidBook();
            var bookRepository = new Mock<IBookRepository>(MockBehavior.Strict);
            bookRepository
                .Setup(repo => repo.Register(book))
                .Verifiable();
            bookRepository
                .Setup(repo => repo.HasAny(book))
                .Returns(false);
            IBookServices bookServices = new BookServices(bookRepository.Object);

            bookServices.Register(book);

            bookServices.IsValid.Should().BeTrue();
            bookRepository.Verify();
        }

        [Fact]
        public void ShouldBecomeInvalidWhenBookIsAlreadyRegistered()
        {
            var book = GetValidBook();
            var bookRepository = new Mock<IBookRepository>();
            bookRepository
                .Setup(repo => repo.HasAny(book))
                .Returns(true);
            IBookServices bookServices = new BookServices(bookRepository.Object);

            bookServices.Register(book);
            var operationValid = bookServices.IsValid;
            var errorMessages = bookServices.GetErrorMessages();

            bookServices.Register(book);
            operationValid.Should().BeFalse();
            errorMessages.Should().Be($"The Book \"{book.Title}\" is already registered");
            bookRepository.VerifyAll();
        }

        [Fact]
        public void ShouldBecomeInvalidWhenBookIsInvalid()
        {
            var book = GetInvalidBook();
            var bookRepository = new Mock<IBookRepository>(MockBehavior.Strict);
            IBookServices bookServices = new BookServices(bookRepository.Object);

            bookServices.Register(book);
            var operationValid = bookServices.IsValid;
            var errorMessages = bookServices.GetErrorMessages();

            operationValid.Should().BeFalse();
            bookRepository.VerifyAll();
        }

        private Book GetValidBook()
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

        private Book GetInvalidBook()
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
    }
}
