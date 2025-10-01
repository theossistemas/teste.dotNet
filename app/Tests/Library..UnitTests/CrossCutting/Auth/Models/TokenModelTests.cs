using FluentAssertions;
using Library.CrossCutting.Auth.Models;

namespace Library.UnitTests.CrossCutting.Auth.Models;

public class TokenModelTests
{
    [Fact]
    public void Constructor_ShouldInitializeTokenWithDefault()
    {
        // Arrange & Act
        var model = new TokenModel();

        // Assert
        model.Token.Should().BeNullOrEmpty();
    }

    [Fact]
    public void ShouldAllowSettingAndGettingToken()
    {
        // Arrange
        var model = new TokenModel();

        // Act
        model.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";

        // Assert
        model.Token.Should().Be("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9");
    }
}