using FluentAssertions;
using Moq;
using TheosBookStore.Stock.App.Factories;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.App.Services;
using TheosBookStore.Stock.App.Services.Impl;
using TheosBookStore.Stock.App.Tests.Fixtures;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.Services;
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
            BookInsertRequest bookRequest = _bookFixtures.GetValidBookInsertRequest();
            var bookFactory = new Mock<IBookFactory>(MockBehavior.Strict);
            bookFactory
                .Setup(factory => factory.FromInsertRequest(bookRequest))
                .Returns(_bookFixtures.GetValidBook);
            var bookService = new Mock<IBookServices>(MockBehavior.Strict);
            bookService
                .Setup(bs => bs.Register(_bookFixtures.GetValidBook()))
                .Verifiable();
            IBookRegister bookRegister = new BookRegister(
                bookFactory.Object,
                bookService.Object);

            bookRegister.Register(bookRequest);

            bookRegister.IsValid.Should().BeTrue();
            bookService.VerifyAll();
            bookFactory.VerifyAll();
        }

        [Fact]
        public void ShouldNotRegisterWhenBookIsInvalid()
        {
            BookInsertRequest bookRequest = _bookFixtures.GetValidBookInsertRequest();
            var bookFactory = new Mock<IBookFactory>(MockBehavior.Strict);
            bookFactory
                .Setup(factory => factory.FromInsertRequest(bookRequest))
                .Returns(_bookFixtures.GetInvalidBook);
            var bookService = new Mock<IBookServices>(MockBehavior.Strict);

            IBookRegister bookRegister = new BookRegister(
                bookFactory.Object,
                bookService.Object);

            bookRegister.Register(bookRequest);

            bookRegister.IsValid.Should().BeFalse();
            bookService.VerifyAll();
            bookFactory.VerifyAll();
        }
    }
}
