using Library.Application.MediatR.Books.Commands.Delete;
using Library.Shared.Messages;
using FluentValidation.TestHelper;

namespace Library.UnitTests.Application.MediatR.Commands.Books.Commands.Delete;

public class BookDeleteValidatorTests
{
    private readonly BookDeleteValidator _validator;

    public BookDeleteValidatorTests()
    {
        _validator = new BookDeleteValidator();
    }

    [Fact]
    public void Should_Pass_When_Id_Is_Valid_Guid()
    {
        // Arrange
        var command = new BookDeleteCommand(Guid.NewGuid().ToString());

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_Fail_When_Id_Is_Empty()
    {
        // Arrange
        var command = new BookDeleteCommand(string.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
              .WithErrorMessage(FluentValidationMessages.INVALID_VALUE.Replace("{PropertyName}", "Id"));
    }

    [Fact]
    public void Should_Fail_When_Id_Is_Invalid_Guid()
    {
        // Arrange
        var command = new BookDeleteCommand("not-a-guid");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
              .WithErrorMessage(FluentValidationMessages.INVALID_VALUE.Replace("{PropertyName}", "Id"));
    }
}