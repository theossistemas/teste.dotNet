using FluentValidation;
using FluentValidation.Results;
using Moq;
using TheosLivraria.Application.UseCases.Book.Create;
using TheosLivraria.Application.UseCases.Book.Create.DTOs;
using TheosLivraria.Domain.Interfaces.Data;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;
using Xunit;

public class CreateBookUseCaseTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IValidator<CreateBookInput>> _validatorMock = new();

    private CreateBookUseCase GetUseCase()
        => new CreateBookUseCase(_bookRepositoryMock.Object, _unitOfWorkMock.Object, _validatorMock.Object);

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFail_WhenValidationFails()
    {
        var input = new CreateBookInput
        {
            Title = "",
            Author = "",
            Description = "",
            StockQuantity = 0,
            Price = 0,
            PublishedDate = DateTime.UtcNow
        };

        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Title", "Título é obrigatório"),
            new ValidationFailure("Author", "Autor é obrigatório")
        };

        _validatorMock.Setup(v => v.ValidateAsync(input, default))
            .ReturnsAsync(new ValidationResult(failures));

        var useCase = GetUseCase();

        var result = await useCase.ExecuteAsync(input);

        Assert.False(result.Success);
        Assert.Equal("Erro de validação na entidade.", result.ErrorMessage);

        var errors = result.MetaData as IEnumerable<object>;
        Assert.NotNull(errors);
        Assert.NotEmpty(errors);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFail_WhenBookTitleAlreadyExists()
    {
        var input = new CreateBookInput { Title = "Existing Book" };
        _validatorMock.Setup(v => v.ValidateAsync(input, default))
            .ReturnsAsync(new ValidationResult());

        _bookRepositoryMock.Setup(r => r.GetByTitleAsync(input.Title))
            .ReturnsAsync(new BookEntity());

        var useCase = GetUseCase();

        var result = await useCase.ExecuteAsync(input);

        Assert.False(result.Success);
        Assert.Equal("O título do livro informado já está em uso.", result.ErrorMessage);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenInputIsValid()
    {
        var input = new CreateBookInput
        {
            Title = "New Book",
            Author = "Author",
            Description = "Description",
            StockQuantity = 10,
            Price = 25.5m,
            PublishedDate = DateTime.UtcNow
        };

        _validatorMock.Setup(v => v.ValidateAsync(input, default))
            .ReturnsAsync(new ValidationResult());

        _bookRepositoryMock.Setup(r => r.GetByTitleAsync(input.Title))
            .ReturnsAsync((BookEntity?)null);

        var useCase = GetUseCase();

        var result = await useCase.ExecuteAsync(input);

        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.NotEqual(Guid.Empty, result.Value.Id);
    }
}
