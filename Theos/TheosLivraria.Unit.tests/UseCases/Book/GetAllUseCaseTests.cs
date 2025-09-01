
using AutoBogus;
using Moq;
using TheosLivraria.Application.UseCases.Book.GetAll;
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.GetAll;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;
using Xunit;

namespace TheosLivraria.Unit.Tests.UseCases.BookEntityEntity
{
    public class GetAllUseCaseTests
    {
        private readonly Mock<IBookRepository> _BookEntityEntityRepositoryMock;
        private readonly GetAllUseCase _sut;

        public GetAllUseCaseTests()
        {
            _BookEntityEntityRepositoryMock = new Mock<IBookRepository>();
            _sut = new GetAllUseCase(_BookEntityEntityRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_Should_ReturnMappedViewModels_WhenBookEntityEntitysExist()
        {
            // Arrange
            var fakeBookEntityEntitys = new AutoFaker<BookEntity>()
                .RuleFor(b => b.Active, f => true)
                .Generate(5);

            _BookEntityEntityRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BookEntity, bool>>>()))
                .ReturnsAsync(fakeBookEntityEntitys);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fakeBookEntityEntitys.Count, result.Count);
            Assert.All(result, item => Assert.IsType<ResponseBookAllViewModel>(item));

            _BookEntityEntityRepositoryMock.Verify(r => r.GetAllAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BookEntity, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_ReturnEmptyList_WhenNoBookEntityEntitysExist()
        {
            // Arrange
            _BookEntityEntityRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BookEntity, bool>>>()))
                .ReturnsAsync(new List<BookEntity>());

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _BookEntityEntityRepositoryMock.Verify(r => r.GetAllAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BookEntity, bool>>>()), Times.Once);
        }
    }
}
