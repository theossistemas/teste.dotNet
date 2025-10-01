using FluentValidation.TestHelper;
using Library.Application.MediatR.Authenticate.Commands.SignUp;
using Library.Shared.Messages;

namespace Library.UnitTests.Application.MediatR.Commands.Auhenticate.SignUp;

public class SignUpValidatorTests
{
    private readonly SignUpValidator _validator;

    public SignUpValidatorTests()
    {
        _validator = new SignUpValidator();
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Is_Empty()
    {
        // Arrange
        var command = new SignUpCommand
        {
            UserName = "",
            Password = "validPassword"
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserName)
              .WithErrorMessage(FluentValidationMessages.NOT_EMPTY.Replace("{PropertyName}", "User Name"));
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        // Arrange
        var command = new SignUpCommand
        {
            UserName = "validUser",
            Password = "validPassword"
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage(FluentValidationMessages.NOT_EMPTY.Replace("{PropertyName}", "Email"));
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        // Arrange
        var command = new SignUpCommand
        {
            UserName = "validUser",
            Password = ""
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Password)
              .WithErrorMessage(FluentValidationMessages.NOT_EMPTY.Replace("{PropertyName}", "Password"));
    }

    [Fact]
    public void Should_Not_Have_Error_When_UserName_Password_And_Email_Are_Valid()
    {
        // Arrange
        var command = new SignUpCommand
        {
            UserName = "validUser",
            Email = "validEmail",
            Password = "validPassword"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.UserName);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }
}