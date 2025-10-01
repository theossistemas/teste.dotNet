using Library.Application.MediatR.Authenticate.Commands.SignUp;
using Library.UnitTests.Application.MediatR.Commands.Auhenticate.SignIn;

namespace Library.UnitTests.Application.MediatR.Commands.Auhenticate.SignUp;

public class SignUpResultTests
{
    [Fact]
    public void CanCreateSignInResult_WithToken()
    {
        // Arrange
        var token = "sample-token";

        // Act
        var result = new SignUpResult(token);

        // Assert
        Assert.Equal(token, result.Token);
    }

    [Fact]
    public void Records_WithSameToken_AreEqual()
    {
        // Arrange
        var token1 = "token123";
        var token2 = "token123";

        // Act
        var result1 = new SignUpResult(token1);
        var result2 = new SignUpResult(token2);

        // Assert
        Assert.Equal(result1, result2);
    }

    [Fact]
    public void Records_WithDifferentToken_AreNotEqual()
    {
        // Arrange
        var result1 = new SignUpResult("token1");
        var result2 = new SignUpResult("token2");

        // Assert
        Assert.NotEqual(result1, result2);
    }
}