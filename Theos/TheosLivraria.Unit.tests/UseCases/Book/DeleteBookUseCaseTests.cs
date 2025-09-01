
using AutoBogus;
using Microsoft.Extensions.Logging;
using Moq;
using TheosLivraria.Application.UseCases.Book.Delete;
using TheosLivraria.Application.UseCases.Book.Update;
using TheosLivraria.Domain.Interfaces.Logs;
using TheosLivraria.Domain.Interfaces.Notification;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Interfaces.UnitOfWork;
using TheosLivraria.Domain.Models.Entities;
using Xunit;

namespace TheosLivraria.Unit.Tests.UseCases.Book
{
    public class DeleteBookUseCaseTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IDomainNotification> _domainNotificationMock;
        private readonly Mock<ILogger<UpdateBookUseCase>> _loggerMock;
        private readonly Mock<IAuditService> _auditServiceMock;

        private readonly DeleteBookUseCase _sut;

        public DeleteBookUseCaseTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _domainNotificationMock = new Mock<IDomainNotification>();
            _loggerMock = new Mock<ILogger<UpdateBookUseCase>>();
            _auditServiceMock = new Mock<IAuditService>();

            _sut = new DeleteBookUseCase(
                _bookRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _domainNotificationMock.Object,
                _loggerMock.Object,
                _auditServiceMock.Object
            );
        }

        [Fact]
        public async Task Execute_Should_SetActiveFalse_And_Update_WhenBookExists()
        {
            // Arrange
            var fakeBook = new AutoFaker<BookEntity>().Generate();
            fakeBook.Active = true;
            _bookRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                               .ReturnsAsync(fakeBook);

            // Act
            await _sut.Execute(fakeBook.Id);

            // Assert
            Assert.False(fakeBook.Active);
            _bookRepositoryMock.Verify(r => r.Update(fakeBook), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
            _auditServiceMock.Verify(a => a.LogInfoAsync(
                It.Is<string>(msg => msg.Contains("Livro deletado")),
                "DeleteBookUseCase",
                It.IsAny<string>(),
                fakeBook
            ), Times.Once);
        }

        [Fact]
        public async Task Execute_Should_AddNotification_WhenBookDoesNotExist()
        {
            // Arrange
            _bookRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                               .ReturnsAsync((BookEntity?)null);

            // Act
            await _sut.Execute(123);

            // Assert
            _domainNotificationMock.Verify(d => d.AddNotification("Id", "Book not found."), Times.Once);
            _bookRepositoryMock.Verify(r => r.Update(It.IsAny<BookEntity>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Execute_Should_LogError_WhenExceptionIsThrown()
        {
            // Arrange
            var fakeBook = new AutoFaker<BookEntity>().Generate();
            _bookRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                               .ReturnsAsync(fakeBook);

            _bookRepositoryMock.Setup(r => r.Update(It.IsAny<BookEntity>()))
                               .Throws(new Exception("DB error"));

            // Act
            await _sut.Execute(fakeBook.Id);

            // Assert
            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => true),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once
            );

            _auditServiceMock.Verify(a => a.LogErrorAsync(
                It.IsAny<Exception>(),
                "DeleteBookUseCase",
                It.IsAny<string>(),
                It.IsAny<object>()
            ), Times.Once);
        }
    }
}
