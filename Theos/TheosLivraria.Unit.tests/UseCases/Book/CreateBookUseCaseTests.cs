
using AutoBogus;
using Microsoft.Extensions.Logging;
using Moq;
using TheosLivraria.Application.UseCases.Book.Create;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.CreateBook;
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.Create;
using TheosLivraria.Domain.Interfaces.Logs;
using TheosLivraria.Domain.Interfaces.Notification;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Interfaces.UnitOfWork;
using Xunit;

namespace TheosLivraria.Unit.tests.UseCases.Book
{
    public class CreateBookUseCaseTests
    {
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IDomainNotification> _domainNotification;
        private readonly Mock<ILogger<CreateBookUseCase>> _logger;
        private readonly Mock<IAuditService> _auditService;

        private readonly CreateBookUseCase _useCase;

        public CreateBookUseCaseTests()
        {
            _bookRepository = new Mock<IBookRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _domainNotification = new Mock<IDomainNotification>();
            _logger = new Mock<ILogger<CreateBookUseCase>>();
            _auditService = new Mock<IAuditService>();

            _useCase = new CreateBookUseCase(
                _bookRepository.Object,
                _unitOfWork.Object,
                _domainNotification.Object,
                _logger.Object,
                _auditService.Object
            );
        }

        [Fact]
        public async Task Execute_ShouldCreateBook_WhenValidRequest()
        {
            // Arrange
            var request = new AutoFaker<RequestCreateBookViewModel>()
                .RuleFor(r => r.Name, f => f.Lorem.Sentence(3))
                .RuleFor(r => r.Author, f => f.Person.FullName)
                .Generate();

            _bookRepository.Setup(r => r.AddAsync(It.IsAny<Domain.Models.Entities.BookEntity>()))
                           .Returns(Task.CompletedTask);

            _unitOfWork.Setup(u => u.CommitAsync());

            // Act
            var result = await _useCase.Execute(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseCreateBookViewModel>(result);
            _bookRepository.Verify(r => r.AddAsync(It.IsAny<Domain.Models.Entities.BookEntity>()), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
            _auditService.Verify(a => a.LogInfoAsync(
                It.IsAny<string>(),
                nameof(CreateBookUseCase),
                It.IsAny<string>(),
                It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldReturnNull_WhenValidationFails()
        {
            // Arrange
            var request = new RequestCreateBookViewModel
            {
                Name = "", // inválido
                Author = "" // inválido
            };

            // Act
            var result = await _useCase.Execute(request);

            // Assert
            Assert.Null(result);
            _domainNotification.Verify(d => d.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
            _bookRepository.Verify(r => r.AddAsync(It.IsAny<Domain.Models.Entities.BookEntity>()), Times.Never);
        }

        [Fact]
        public async Task Execute_ShouldReturnNull_WhenExceptionThrown()
        {
            // Arrange
            var request = new AutoFaker<RequestCreateBookViewModel>().Generate();

            _bookRepository.Setup(r => r.AddAsync(It.IsAny<Domain.Models.Entities.BookEntity>()))
                           .ThrowsAsync(new Exception("DB Error"));

            // Act
            var result = await _useCase.Execute(request);

            // Assert
            Assert.Null(result);
            _logger.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once);
            _auditService.Verify(a => a.LogErrorAsync(
                It.IsAny<Exception>(),
                "CreateBookUseCase",
                It.IsAny<string>(),
                request), Times.Once);
        }
    }
}
