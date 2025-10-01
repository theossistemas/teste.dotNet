using MediatR;
using FluentAssertions;
using Library.Application.MediatR.Books.Commands.Delete;

namespace Library.UnitTests.Application.MediatR.Commands.Books.Commands.Delete;

public class BookDeleteCommandTests
{
    [Fact]
    public void Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        var id = "123";

        // Act
        var command = new BookDeleteCommand(id);

        // Assert
        command.Id.Should().Be(id);
        command.Should().BeAssignableTo<IRequest<Unit>>();
    }

    [Fact]
    public void Commands_WithSameId_ShouldBeEqual()
    {
        // Arrange
        var command1 = new BookDeleteCommand("abc");
        var command2 = new BookDeleteCommand("abc");

        // Act & Assert
        command1.Should().Be(command2);
        command1.GetHashCode().Should().Be(command2.GetHashCode());
    }

    [Fact]
    public void Commands_WithDifferentIds_ShouldNotBeEqual()
    {
        // Arrange
        var command1 = new BookDeleteCommand("123");
        var command2 = new BookDeleteCommand("456");

        // Act & Assert
        command1.Should().NotBe(command2);
        command1.GetHashCode().Should().NotBe(command2.GetHashCode());
    }
}