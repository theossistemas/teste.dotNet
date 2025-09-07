using FluentValidation;
using FluentValidation.Results;
using Moq;
using TheosLivraria.Application.UseCases.Book.GetById;
using TheosLivraria.Application.UseCases.Book.GetById.DTOs;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;
using Xunit;

namespace TheosLivraria.Tests.Unit.UseCases.Book
{
    public class GetBookByIdUseCaseTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IValidator<GetBookByIdInput>> _validatorMock;
        private readonly GetBookByIdUseCase _useCase;

        public GetBookByIdUseCaseTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _validatorMock = new Mock<IValidator<GetBookByIdInput>>();
            _useCase = new GetBookByIdUseCase(_bookRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnValidationErrors_WhenInputIsInvalid()
        {
            var input = new GetBookByIdInput { Id = Guid.Empty };

            _validatorMock.Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>
                {
            new ValidationFailure("Id", "O Id do livro é obrigatório.")
                }));

            var result = await _useCase.ExecuteAsync(input);

            Assert.False(result.Success);
            Assert.Equal("Erro de validação na entidade.", result.ErrorMessage);

            if (result.MetaData is not null)
            {
                var errors = result.MetaData as IEnumerable<object>;
                Assert.NotNull(errors);
                Assert.Contains(errors, e => e.ToString()!.Contains("Id"));
            }
        }
        [Fact]
        public async Task ExecuteAsync_ShouldReturnFail_WhenBookNotFound()
        {
            var input = new GetBookByIdInput { Id = Guid.NewGuid() };
            _validatorMock.Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(new ValidationResult());

            _bookRepositoryMock.Setup(r => r.GetByIdAsync(input.Id))
                .ReturnsAsync((BookEntity?)null);

            var result = await _useCase.ExecuteAsync(input);

            Assert.False(result.Success);
            Assert.Equal("Livro não encontrado.", result.ErrorMessage);
            Assert.Null(result.MetaData);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnBook_WhenBookExists()
        {
            var bookId = Guid.NewGuid();
            var input = new GetBookByIdInput { Id = bookId };

            _validatorMock.Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(new ValidationResult());

            var book = new BookEntity
            {
                Id = bookId,
                Title = "Test Book",
                Author = "Author",
                Description = "Description",
                Price = 100,
                StockQuantity = 5,
                PublishedDate = DateTime.UtcNow
            };

            _bookRepositoryMock.Setup(r => r.GetByIdAsync(bookId))
                .ReturnsAsync(book);

            var result = await _useCase.ExecuteAsync(input);

            Assert.True(result.Success);
            Assert.Equal(bookId, result.Value?.Id);
            Assert.Equal("Test Book", result.Value?.Title);
            Assert.Equal("Author", result.Value?.Author);
        }
    }
}
