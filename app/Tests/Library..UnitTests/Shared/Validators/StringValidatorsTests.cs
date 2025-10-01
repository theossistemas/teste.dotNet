using FluentAssertions;
using Library.Shared.Validators;

namespace Library.UnitTests.Shared.Validators;

public class StringValidatorsTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void IsEmpty_ShouldReturnTrue_ForNullOrWhitespace(string value)
    {
        // Act
        bool result = value.IsEmpty();

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("  abc  ")]
    [InlineData("0")]
    public void IsEmpty_ShouldReturnFalse_ForNonEmpty(string value)
    {
        bool result = value.IsEmpty();
        result.Should().BeFalse();
    }

    [Fact]
    public void IsGuid_ShouldReturnTrue_ForValidGuid()
    {
        // Arrange
        string value = Guid.NewGuid().ToString();

        // Act
        bool result = value.IsGuid();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsGuid_ShouldReturnFalse_ForEmptyGuid()
    {
        string value = Guid.Empty.ToString();
        bool result = value.IsGuid();
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid-guid")]
    [InlineData("12345")]
    public void IsGuid_ShouldReturnFalse_ForInvalidGuid(string value)
    {
        bool result = value.IsGuid();
        result.Should().BeFalse();
    }
}