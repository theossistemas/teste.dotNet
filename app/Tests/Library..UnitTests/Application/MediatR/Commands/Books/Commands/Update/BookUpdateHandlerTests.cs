using FluentAssertions;
using Library.Application.MediatR.Books.Commands.Update;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using MediatR;
using Moq;
using System.Linq.Expressions;

namespace Library.UnitTests.Application.MediatR.Commands.Books.Commands.Update;

public class BookUpdateHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly BookUpdateHandler _handler;

    public BookUpdateHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new BookUpdateHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateBook_WhenNameIsUnique()
    {
        // Arrange
        var command = new BookUpdateCommand
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Clean Code",
            Author = "Robert C. Martin",
            Summary = "Livro clássico sobre código limpo."
        };

        // Simula que não existe livro com o mesmo nome
        _bookRepositoryMock
            .Setup(r => r.Count(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Simula que GetById retorna Entity.Book
        _bookRepositoryMock
            .Setup(r => r.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(command);

        // Simula que Update retorna Entity.Book
        _bookRepositoryMock
            .Setup(r => r.Update(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(command);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);

        _bookRepositoryMock.Verify(r => r.Count(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        _bookRepositoryMock.Verify(r => r.Update(It.Is<Book>(b =>
            b.Name == command.Name &&
            b.Author == command.Author &&
            b.Summary == command.Summary
        ), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenNameAlreadyExists()
    {
        // Arrange
        var command = new BookUpdateCommand
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Clean Architecture",
            Author = "Robert C. Martin",
            Summary = "Outro livro do Uncle Bob."
        };

        // Simula que já existe livro com o mesmo nome
        _bookRepositoryMock
            .Setup(r => r.Count(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"*nome ({command.Name})*");

        _bookRepositoryMock.Verify(r => r.Count(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        _bookRepositoryMock.Verify(r => r.Update(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}