using Library.Application.MediatR.Authenticate.Commands.SignIn;

namespace Library.UnitTests.Application.MediatR.Commands.Auhenticate.SignIn;

public class SignInResultTests
{
    [Fact]
    public void CanCreateSignInResult_WithToken()
    {
        // Arrange
        var token = "sample-token";

        // Act
        var result = new SignInResult(token);

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
        var result1 = new SignInResult(token1);
        var result2 = new SignInResult(token2);

        // Assert
        Assert.Equal(result1, result2);
    }

    [Fact]
    public void Records_WithDifferentToken_AreNotEqual()
    {
        // Arrange
        var result1 = new SignInResult("token1");
        var result2 = new SignInResult("token2");

        // Assert
        Assert.NotEqual(result1, result2);
    }
}