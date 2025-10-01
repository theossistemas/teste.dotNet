using Library.Application.MediatR.Authenticate.Commands.SignUp;
using Library.Application.MediatR.Books.Commands.Create;
using Library.Domain.Entities;

namespace Library.UnitTests.Application.MediatR.Commands.Books.Commands.Create;
public class BookCreateCommandTests
{
    [Fact]
    public void Records_WithSameValues_AreEqual()
    {
        var cmd1 = new BookCreateCommand
        {
            Name = "O Senhor dos Anéis",
            Author = "J.R.R. Tolkien",
            Summary = "A jornada épica pela Terra-média"
        };
        var cmd2 = new BookCreateCommand
        {
            Name = "O Senhor dos Anéis",
            Author = "J.R.R. Tolkien",
            Summary = "A jornada épica pela Terra-média"
        };

        Assert.Equal(cmd1, cmd2);
    }

    [Fact]
    public void BookCreateCommand_Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var command = new BookCreateCommand
        {
            Name = "O Senhor dos Anéis",
            Author = "J.R.R. Tolkien",
            Summary = "A jornada épica pela Terra-média"
        };

        // Act & Assert
        Assert.Equal("O Senhor dos Anéis", command.Name);
        Assert.Equal("J.R.R. Tolkien", command.Author);
        Assert.Equal("A jornada épica pela Terra-média", command.Summary);
    }

    [Fact]
    public void ImplicitOperator_ShouldConvertToBook()
    {
        // Arrange
        var command = new BookCreateCommand
        {
            Name = "O Senhor dos Anéis",
            Author = "J.R.R. Tolkien",
            Summary = "A jornada épica pela Terra-média"
        };

        // Act
        Book book = command; // usa o operador implícito

        // Assert
        Assert.Equal(command.Name, book.Name);
        Assert.Equal(command.Author, book.Author);
        Assert.Equal(command.Summary, book.Summary);
        Assert.Equal(Guid.Empty, book.Id); // garante que o Id foi gerado
    }
}