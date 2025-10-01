using FluentAssertions;
using Library.Shared.Extensions;

namespace Library.UnitTests.Shared.Extensions;

public class StringExtensionsTests
{
    [Fact]
    public void GuidParse_ShouldReturnGuid_WhenValueIsValid()
    {
        // Arrange
        var guid = Guid.NewGuid();
        string value = guid.ToString();

        // Act
        var result = value.GuidParse();

        // Assert
        result.Should().Be(guid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GuidParse_ShouldThrowArgumentException_WhenValueIsNullOrEmpty(string value)
    {
        // Act
        Action act = () => value.GuidParse();

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("O valor não pode ser nulo ou vazio.*");
    }

    [Fact]
    public void GuidParse_ShouldThrowFormatException_WhenValueIsInvalid()
    {
        // Arrange
        string value = "invalid-guid";

        // Act
        Action act = () => value.GuidParse();

        // Assert
        act.Should().Throw<FormatException>()
            .WithMessage("O valor 'invalid-guid' não é um GUID válido.");
    }
}