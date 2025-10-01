using Library.Application.MediatR.Authenticate.Commands.SignIn;

namespace Library.UnitTests.Application.MediatR.Commands.Auhenticate.SignIn;

public class SignInCommandTests
{
    [Fact]
    public void Records_WithSameValues_AreEqual()
    {
        var cmd1 = new SignInCommand { UserName = "u", Password = "p" };
        var cmd2 = new SignInCommand { UserName = "u", Password = "p" };

        Assert.Equal(cmd1, cmd2);
    }


    [Fact]
    public void CanCreateSignInCommand_WithProperties()
    {
        // Arrange
        var username = "testuser";
        var password = "password123";

        // Act
        var command = new SignInCommand
        {
            UserName = username,
            Password = password
        };

        // Assert
        Assert.Equal(username, command.UserName);
        Assert.Equal(password, command.Password);
    }

    [Fact]
    public void CanCreateSignInCommand_WithRecordInitialization()
    {
        // Arrange & Act
        var command = new SignInCommand
        {
            UserName = "anotheruser",
            Password = "secretpass"
        };

        // Assert
        Assert.Equal("anotheruser", command.UserName);
        Assert.Equal("secretpass", command.Password);
    }
}