using FluentAssertions;
using Moq;
using TheosBookStore.Stock.App.Factories;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.App.Services;
using TheosBookStore.Stock.App.Services.Impl;
using TheosBookStore.Stock.App.Tests.Fixtures;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.Repositories;
using TheosBookStore.Stock.Domain.Services;
using TheosBookStore.Stock.Domain.ValueObjects;
using Xunit;

namespace TheosBookStore.Stock.App.Tests.Services
{
    public class BookRegisterTest : IClassFixture<BookTestFixtures>
    {
        private readonly BookTestFixtures _bookFixtures;

        public BookRegisterTest(BookTestFixtures bookFixtures)
        {
            _bookFixtures = bookFixtures;
        }

        [Fact]
        public void ShouldRegisterABook()
        {
            BookRequest bookRequest = _bookFixtures.GetValidBookInsertRequest();
            var validBook = _bookFixtures.GetValidBook();
            var bookFactory = new Mock<IBookFactory>(MockBehavior.Strict);
            bookFactory
                .Setup(factory => factory.FromRequest(bookRequest))
                .Returns(validBook);
            bookFactory
                .Setup(factory => factory.FromEntityToResponse(validBook))
                .Returns(new BookResponse());
            var bookService = new Mock<IBookServices>(MockBehavior.Strict);
            bookService
                .Setup(bs => bs.Register(validBook));
            bookService
                .Setup(bs => bs.IsValid)
                .Returns(true);
            var repository = new Mock<IBookRepository>(MockBehavior.Strict);
            repository
                .Setup(repo => repo.GetByISBN(It.IsAny<ISBN>()))
                .Returns(validBook);
            IBookRegister bookRegister = new BookRegister(
                bookFactory.Object,
                bookService.Object,
                repository.Object);

            bookRegister.Execute(bookRequest);

            bookRegister.IsValid.Should().BeTrue();
            bookService.VerifyAll();
            bookFactory.VerifyAll();
            repository.VerifyAll();
        }

        [Fact]
        public void ShouldNotRegisterWhenBookIsInvalid()
        {
            BookRequest bookRequest = _bookFixtures.GetValidBookInsertRequest();
            var bookFactory = new Mock<IBookFactory>(MockBehavior.Strict);
            bookFactory
                .Setup(factory => factory.FromRequest(bookRequest))
                .Returns(_bookFixtures.GetInvalidBook);
            var bookService = new Mock<IBookServices>(MockBehavior.Strict);
            var repository = new Mock<IBookRepository>();

            IBookRegister bookRegister = new BookRegister(
                bookFactory.Object,
                bookService.Object,
                repository.Object);

            bookRegister.Execute(bookRequest);

            bookRegister.IsValid.Should().BeFalse();
            bookService.VerifyAll();
            bookFactory.VerifyAll();
        }
    }
}
