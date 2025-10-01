using FluentAssertions;

namespace Library.UnitTests.Shared.AppSettings.Types;

public class JWTSettingsTypeTests
{
    [Fact]
    public void Properties_ShouldSetAndGetValuesCorrectly()
    {
        // Arrange
        var jwtSettings = new JWTSettingsType();
        int expectedExpireHours = 5;
        string expectedSecret = "MySecretKey";

        // Act
        jwtSettings.ExpireHours = expectedExpireHours;
        jwtSettings.Secret = expectedSecret;

        // Assert
        jwtSettings.ExpireHours.Should().Be(expectedExpireHours);
        jwtSettings.Secret.Should().Be(expectedSecret);
    }
}