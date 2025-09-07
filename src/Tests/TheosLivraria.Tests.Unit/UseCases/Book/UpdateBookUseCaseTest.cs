using Moq;
using FluentValidation;
using FluentValidation.Results;
using TheosLivraria.Application.UseCases.Book.Update;
using TheosLivraria.Domain.Interfaces.Data;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Application.UseCases.Book.Create.DTOs;

namespace TheosLivraria.Tests.Application.UseCases.Book
{
    public class UpdateBookUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IValidator<UpdateBookInput>> _validatorMock;
        private readonly UpdateBookUseCase _useCase;

        public UpdateBookUseCaseTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _validatorMock = new Mock<IValidator<UpdateBookInput>>();

            _useCase = new UpdateBookUseCase(
                _bookRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _validatorMock.Object
            );
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFail_WhenValidationFails()
        {
            var input = new UpdateBookInput { Title = "Invalid" };
            var validationResult = new ValidationResult(new[]
            {
                new ValidationFailure("Title", "Título inválido")
            });

            _validatorMock.Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(validationResult);

            var result = await _useCase.ExecuteAsync(input);

            Assert.False(result.Success);
            Assert.Equal("Erro de validação na entidade.", result.ErrorMessage);
            Assert.NotNull(result.MetaData);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFail_WhenBookNotFound()
        {
            var input = new UpdateBookInput { Id = Guid.NewGuid(), Title = "Novo Título" };

            _validatorMock.Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(new ValidationResult());

            _bookRepositoryMock.Setup(r => r.GetByIdAsync(input.Id))
                .ReturnsAsync((BookEntity)null);

            var result = await _useCase.ExecuteAsync(input);

            Assert.False(result.Success);
            Assert.Equal("Livro não encontrado.", result.ErrorMessage);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFail_WhenTitleAlreadyInUseByAnotherBook()
        {
            var bookId = Guid.NewGuid();
            var input = new UpdateBookInput { Id = bookId, Title = "Título Duplicado" };

            var book = new BookEntity { Id = bookId, Title = "Antigo Título" };
            var anotherBook = new BookEntity { Id = Guid.NewGuid(), Title = "Título Duplicado" };

            _validatorMock.Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(new ValidationResult());

            _bookRepositoryMock.Setup(r => r.GetByIdAsync(input.Id))
                .ReturnsAsync(book);

            _bookRepositoryMock.Setup(r => r.GetByTitleAsync(input.Title))
                .ReturnsAsync(anotherBook);

            var result = await _useCase.ExecuteAsync(input);

            Assert.False(result.Success);
            Assert.Equal("O título do livro informado já está em uso.", result.ErrorMessage);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnOk_WhenUpdateIsSuccessful()
        {
            var bookId = Guid.NewGuid();
            var input = new UpdateBookInput
            {
                Id = bookId,
                Title = "Novo Título",
                Author = "Autor Atualizado",
                Description = "Nova descrição",
                StockQuantity = 10,
                Price = 50,
                PublishedDate = DateTime.UtcNow.AddYears(-1)
            };

            var existingBook = new BookEntity { Id = bookId, Title = "Título Antigo" };

            _validatorMock.Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(new ValidationResult());

            _bookRepositoryMock.Setup(r => r.GetByIdAsync(input.Id))
                .ReturnsAsync(existingBook);

            _bookRepositoryMock.Setup(r => r.GetByTitleAsync(input.Title))
                .ReturnsAsync((BookEntity)null);

            var result = await _useCase.ExecuteAsync(input);

            Assert.True(result.Success);
            Assert.NotNull(result.Value);
            Assert.Equal(bookId, result.Value.Id);

            _bookRepositoryMock.Verify(r => r.Update(existingBook), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
