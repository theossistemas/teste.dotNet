using FluentValidation.TestHelper;
using Library.Application.MediatR.Authenticate.Commands.SignIn;
using Library.Shared.Messages;

namespace Library.UnitTests.Application.MediatR.Commands.Auhenticate.SignIn;

public class SignInValidatorTests
{
    private readonly SignInValidator _validator;

    public SignInValidatorTests()
    {
        _validator = new SignInValidator();
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Is_Empty()
    {
        // Arrange
        var command = new SignInCommand
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
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        // Arrange
        var command = new SignInCommand
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
    public void Should_Not_Have_Error_When_UserName_And_Password_Are_Valid()
    {
        // Arrange
        var command = new SignInCommand
        {
            UserName = "validUser",
            Password = "validPassword"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.UserName);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }
}