
using AutoBogus;
using Moq;
using TheosLivraria.Application.UseCases.Book.GetAllPaged;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.GetAllPaged;
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.GetAllPaged;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Domain.Models.Filters.Book;
using Xunit;

namespace TheosLivraria.Unit.Tests.UseCases.Book
{
    public class GetAllPagedUseCaseTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly GetAllPagedUseCase _sut;

        public GetAllPagedUseCaseTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _sut = new GetAllPagedUseCase(_bookRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_Should_ReturnPagedViewModel_WhenBooksExist()
        {
            // Arrange
            var request = new RequestBookAllPagedViewModel { Page = 1, Size = 5 };
            var fakeBooks = new AutoFaker<BookEntity>()
                .RuleFor(b => b.Active, f => true)
                .Generate(5);

            int totalCount = 20;

            _bookRepositoryMock
                .Setup(r => r.GetBooksPagedAsync(It.IsAny<BookAllPagedFilter>()))
                .ReturnsAsync((fakeBooks, totalCount));

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(totalCount, result.TotalCount);
            Assert.Equal(request.Page, result.Page);
            Assert.Equal(request.Size, result.Size);
            Assert.Equal(fakeBooks.Count, result.Items.Count);
            Assert.All(result.Items, item => Assert.IsType<ResponseBookAllPagedViewModel>(item));

            _bookRepositoryMock.Verify(r => r.GetBooksPagedAsync(It.IsAny<BookAllPagedFilter>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_ReturnEmptyPagedViewModel_WhenNoBooksExist()
        {
            // Arrange
            var request = new RequestBookAllPagedViewModel { Page = 1, Size = 5 };

            _bookRepositoryMock
                .Setup(r => r.GetBooksPagedAsync(It.IsAny<BookAllPagedFilter>()))
                .ReturnsAsync((new List<BookEntity>(), 0));

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Items);
            Assert.Equal(0, result.TotalCount);
            Assert.Equal(request.Page, result.Page);
            Assert.Equal(request.Size, result.Size);

            _bookRepositoryMock.Verify(r => r.GetBooksPagedAsync(It.IsAny<BookAllPagedFilter>()), Times.Once);
        }
    }
}
