using FluentValidation;
using FluentValidation.Results;
using Moq;
using TheosLivraria.Application.UseCases.Book.list;
using TheosLivraria.Application.UseCases.Book.list.DTOs;
using TheosLivraria.Domain.Interfaces.Repository;
using Xunit;

namespace TheosLivraria.Tests.Unit.UseCases.Book
{
    public class ListBooksUseCaseTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IValidator<ListBooksInput>> _validatorMock;
        private readonly ListBooksUseCase _useCase;

        public ListBooksUseCaseTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _validatorMock = new Mock<IValidator<ListBooksInput>>();
            _useCase = new ListBooksUseCase(_bookRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnValidationErrors_WhenInputIsInvalid()
        {
            var input = new ListBooksInput { Page = 0, PageSize = 0 };

            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Page", "Page deve ser maior que 0"),
                new ValidationFailure("PageSize", "PageSize deve ser maior que 0")
            });

            _validatorMock
                .Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(validationResult);

            var result = await _useCase.ExecuteAsync(input);

            Assert.False(result.Success);
            Assert.Equal("Erro de validação na entidade.", result.ErrorMessage);

            var errors = result.MetaData as IEnumerable<object>;
            Assert.NotNull(errors);
            Assert.Equal(2, errors.Count());
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnBooks_WhenInputIsValid()
        {
            var input = new ListBooksInput { Page = 1, PageSize = 10 };

            _validatorMock
                .Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(new ValidationResult());

            _bookRepositoryMock
                .Setup(r => r.ListBooksAsync(It.IsAny<Domain.Models.Filters.Book.ListBookFilter>()))
                .ReturnsAsync((new List<Domain.Models.Entities.BookEntity>
                {
                    new Domain.Models.Entities.BookEntity
                    {
                        Id = Guid.NewGuid(),
                        Title = "Book 1",
                        Author = "Author 1",
                        Description = "Desc 1",
                        Price = 100,
                        StockQuantity = 5,
                        PublishedDate = DateTime.UtcNow
                    }
                }, 1));

            var result = await _useCase.ExecuteAsync(input);

            Assert.True(result.Success);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.TotalCount);
            Assert.Single(result.Value.Data);
            Assert.Equal("Book 1", result.Value.Data[0].Title);
        }
    }
}
