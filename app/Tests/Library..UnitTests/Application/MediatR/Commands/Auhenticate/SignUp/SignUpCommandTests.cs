using Library.Application.MediatR.Authenticate.Commands.SignIn;
using Library.Application.MediatR.Authenticate.Commands.SignUp;
using Library.Domain.Entities;
using Library.Shared.Extensions;

namespace Library.UnitTests.Application.MediatR.Commands.Auhenticate.SignUp;

public class SignUpCommandTests
{
    [Fact]
    public void Records_WithSameValues_AreEqual()
    {
        var cmd1 = new SignUpCommand { UserName = "u", Password = "p" };
        var cmd2 = new SignUpCommand { UserName = "u", Password = "p" };

        Assert.Equal(cmd1, cmd2);
    }

    [Fact]
    public void SignUpCommand_Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var command = new SignUpCommand
        {
            UserName = "testuser",
            Email = "test@email.com",
            Password = "Password123"
        };

        // Act & Assert
        Assert.Equal("testuser", command.UserName);
        Assert.Equal("test@email.com", command.Email);
        Assert.Equal("Password123", command.Password);
    }

    [Fact]
    public void ImplicitOperator_ShouldConvertToLogin()
    {
        // Arrange
        var command = new SignUpCommand
        {
            UserName = "testuser",
            Email = "test@email.com",
            Password = "Password123"
        };

        // Act
        Login login = command; // usa o operador implícito

        // Assert
        Assert.Equal(command.UserName, login.UserName);
        Assert.Equal(command.Email, login.Email);
        Assert.NotNull(login.PasswordHash);
        Assert.True(EncrypterExtensions.IsValidPassword(command.Password, login.PasswordHash));
    }
}