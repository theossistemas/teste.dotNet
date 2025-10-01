using FluentAssertions;
using Library.Shared.Extensions;

namespace Library.UnitTests.Shared.Extensions;

public class EncrypterExtensionsTests
{
    [Fact]
    public void HashPassword_ShouldReturnHashedValue()
    {
        // Arrange
        string password = "MyPassword123";

        // Act
        string hash = EncrypterExtensions.HashPassword(password);

        // Assert
        hash.Should().NotBeNullOrEmpty();
        hash.Should().NotBe(password); // hash deve ser diferente da senha original
    }

    [Fact]
    public void IsValidPassword_ShouldReturnTrue_ForCorrectPassword()
    {
        // Arrange
        string password = "MyPassword123";
        string hash = EncrypterExtensions.HashPassword(password);

        // Act
        bool isValid = EncrypterExtensions.IsValidPassword(password, hash);

        // Assert
        isValid.Should().BeTrue();
    }

    [Fact]
    public void IsValidPassword_ShouldReturnFalse_ForIncorrectPassword()
    {
        // Arrange
        string password = "MyPassword123";
        string hash = EncrypterExtensions.HashPassword(password);

        // Act
        bool isValid = EncrypterExtensions.IsValidPassword("WrongPassword", hash);

        // Assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void HashPassword_WithCustomSalt_ShouldGenerateValidHash()
    {
        // Arrange
        string password = "MyPassword123";
        int customSalt = 12;

        // Act
        string hash = EncrypterExtensions.HashPassword(password, customSalt);
        bool isValid = EncrypterExtensions.IsValidPassword(password, hash);

        // Assert
        hash.Should().NotBeNullOrEmpty();
        isValid.Should().BeTrue();
    }
}