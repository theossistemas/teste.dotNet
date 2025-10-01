using FluentAssertions;
using Library.Application.MediatR.Books.Commands.Update;
using Library.Domain.Entities;
using MediatR;

namespace Library.UnitTests.Application.MediatR.Commands.Books.Commands.Update;

public class BookUpdateCommandTests
{
    [Fact]
    public void Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        var id = "1";
        var name = "Clean Code";
        var author = "Robert C. Martin";
        var summary = "Um guia para escrever código limpo.";

        // Act
        var command = new BookUpdateCommand
        {
            Id = id,
            Name = name,
            Author = author,
            Summary = summary
        };

        // Assert
        command.Id.Should().Be(id);
        command.Name.Should().Be(name);
        command.Author.Should().Be(author);
        command.Summary.Should().Be(summary);
        command.Should().BeAssignableTo<IRequest<Unit>>();
    }

    [Fact]
    public void ImplicitOperator_ShouldConvertToBook()
    {
        // Arrange
        var command = new BookUpdateCommand
        {
            Id = "123",
            Name = "Domain-Driven Design",
            Author = "Eric Evans",
            Summary = "Livro clássico sobre DDD."
        };

        // Act
        Book book = command; // conversão implícita

        // Assert
        book.Should().NotBeNull();
        book.Name.Should().Be(command.Name);
        book.Author.Should().Be(command.Author);
        book.Summary.Should().Be(command.Summary);
    }

    [Fact]
    public void Commands_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var command1 = new BookUpdateCommand
        {
            Id = "1",
            Name = "Refactoring",
            Author = "Martin Fowler",
            Summary = "Melhorando código existente."
        };

        var command2 = new BookUpdateCommand
        {
            Id = "1",
            Name = "Refactoring",
            Author = "Martin Fowler",
            Summary = "Melhorando código existente."
        };

        // Act & Assert
        command1.Should().Be(command2);
        command1.GetHashCode().Should().Be(command2.GetHashCode());
    }

    [Fact]
    public void Commands_WithDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var command1 = new BookUpdateCommand { Id = "1", Name = "Book A", Author = "Author A", Summary = "Summary A" };
        var command2 = new BookUpdateCommand { Id = "2", Name = "Book B", Author = "Author B", Summary = "Summary B" };

        // Act & Assert
        command1.Should().NotBe(command2);
        command1.GetHashCode().Should().NotBe(command2.GetHashCode());
    }
}