
namespace TheosLivraria.Unit.Tests.UseCases.Book
{
    using System.Threading.Tasks;
    using AutoBogus;
    using global::TheosLivraria.Application.UseCases.Book.Update;
    using global::TheosLivraria.Application.UseCases.ViewModels.Request.Book.Update;
    using global::TheosLivraria.Domain.Interfaces.Logs;
    using global::TheosLivraria.Domain.Interfaces.Notification;
    using global::TheosLivraria.Domain.Interfaces.Repository;
    using global::TheosLivraria.Domain.Interfaces.UnitOfWork;
    using global::TheosLivraria.Domain.Models.Entities;
    using global::TheosLivraria.Domain.Models.Enum;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    namespace TheosLivraria.Tests.UseCases.Book
    {
        public class UpdateBookUseCaseTests
        {
            private readonly Mock<IBookRepository> _bookRepositoryMock;
            private readonly Mock<IUnitOfWork> _unitOfWorkMock;
            private readonly Mock<IDomainNotification> _domainNotificationMock;
            private readonly Mock<ILogger<UpdateBookUseCase>> _loggerMock;
            private readonly Mock<IAuditService> _auditServiceMock;
            private readonly UpdateBookUseCase _sut;

            public UpdateBookUseCaseTests()
            {
                _bookRepositoryMock = new Mock<IBookRepository>();
                _unitOfWorkMock = new Mock<IUnitOfWork>();
                _domainNotificationMock = new Mock<IDomainNotification>();
                _loggerMock = new Mock<ILogger<UpdateBookUseCase>>();
                _auditServiceMock = new Mock<IAuditService>();

                _sut = new UpdateBookUseCase(
                    _bookRepositoryMock.Object,
                    _unitOfWorkMock.Object,
                    _domainNotificationMock.Object,
                    _loggerMock.Object,
                    _auditServiceMock.Object
                );
            }

            [Fact]
            public async Task Execute_Should_UpdateBook_WhenValid()
            {
                // Arrange
                var bookId = 1;
                var existingBook = new AutoFaker<BookEntity>().Generate();
                existingBook.Id = bookId;
                _bookRepositoryMock.Setup(r => r.GetByIdAsync(bookId))
                                   .ReturnsAsync(existingBook);

                var request = new AutoFaker<RequestUpdateBookViewModel>().Generate();
                request.StatusId = (int)StatusEnum.Rented;

                // Act
                await _sut.Execute(bookId, request);

                // Assert
                _bookRepositoryMock.Verify(r => r.Update(It.Is<BookEntity>(b =>
                    b.Id == bookId &&
                    b.Name == request.Name &&
                    b.Author == request.Author &&
                    b.StatusId == request.StatusId
                )), Times.Once);

                _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
                _auditServiceMock.Verify(a => a.LogInfoAsync(It.IsAny<string>(), "UpdateBookUseCase", It.IsAny<string>(), existingBook), Times.Once);
            }

            [Fact]
            public async Task Execute_Should_NotUpdate_WhenBookNotFound()
            {
                // Arrange
                var bookId = 999;
                _bookRepositoryMock.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync((BookEntity?)null);
                var request = new AutoFaker<RequestUpdateBookViewModel>().Generate();

                // Act
                await _sut.Execute(bookId, request);

                // Assert
                _bookRepositoryMock.Verify(r => r.Update(It.IsAny<BookEntity>()), Times.Never);
                _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
                _domainNotificationMock.Verify(d => d.AddNotification("Id", "Book not found."), Times.Once);
            }

            [Fact]
            public async Task Execute_Should_NotUpdate_WhenValidationFails()
            {
                // Arrange
                var bookId = 1;
                var existingBook = new AutoFaker<BookEntity>().Generate();
                existingBook.Id = bookId;

                _bookRepositoryMock.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync(existingBook);

                var request = new RequestUpdateBookViewModel
                {
                    Name = "",
                    Author = "",
                    StatusId = 0
                };

                // Act
                await _sut.Execute(bookId, request);

                // Assert
                _bookRepositoryMock.Verify(r => r.Update(It.IsAny<BookEntity>()), Times.Never);
                _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
                _domainNotificationMock.Verify(d => d.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
            }
        }
    }
}
