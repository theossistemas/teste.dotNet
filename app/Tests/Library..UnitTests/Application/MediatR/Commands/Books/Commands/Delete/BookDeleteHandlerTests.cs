using Moq;
using FluentAssertions;
using MediatR;
using Library.Domain.Interfaces;
using Library.Application.MediatR.Books.Commands.Delete;
using Library.Shared.Extensions;

namespace Library.UnitTests.Application.MediatR.Commands.Books.Commands.Delete;

public class BookDeleteHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly BookDeleteHandler _handler;

    public BookDeleteHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new BookDeleteHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteBookSuccessfully()
    {
        // Arrange
        var command = new BookDeleteCommand(Guid.NewGuid().ToString());

        _bookRepositoryMock
            .Setup(r => r.Delete(Guid.Parse(command.Id), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Equals(Unit.Value).Should().BeTrue();

        _bookRepositoryMock.Verify(
            r => r.Delete(command.Id.GuidParse(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        var command = new BookDeleteCommand(Guid.NewGuid().ToString());

        _bookRepositoryMock
            .Setup(r => r.Delete(command.Id.GuidParse(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException("Livro não encontrado"));

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("Livro não encontrado");

        _bookRepositoryMock.Verify(
            r => r.Delete(command.Id.GuidParse(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}