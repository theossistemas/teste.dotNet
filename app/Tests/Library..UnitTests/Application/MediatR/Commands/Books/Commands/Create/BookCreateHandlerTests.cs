using Library.Application.MediatR.Books.Commands.Create;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Moq;

namespace Library.UnitTests.Application.MediatR.Commands.Books.Commands.Create;

public class BookCreateHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly BookCreateHandler _handler;

    public BookCreateHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new BookCreateHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateBook_ReturnBookCreateResult()
    {
        // Arrange
        var command = new BookCreateCommand
        {
            Name = "O Senhor dos Anéis",
            Author = "J.R.R. Tolkien",
            Summary = "A jornada épica pela Terra-média"
        };

        var book = Book.Create(command.Name, command.Author, command.Summary);

        _bookRepositoryMock
            .Setup(r => r.Insert(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(book.Id, result.Id);
        _bookRepositoryMock.Verify(r => r.Insert(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenBookAlreadyExists_ThrowsArgumentException()
    {
        // Arrange
        var command = new BookCreateCommand
        {
            Name = "O Senhor dos Anéis",
            Author = "J.R.R. Tolkien",
            Summary = "A jornada épica pela Terra-média"
        };

        // Simula que ExistsBookByName lançaria exceção se livro já existe
        _bookRepositoryMock
            .Setup(r => r.Insert(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ArgumentException("Book already exists"));

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }
}