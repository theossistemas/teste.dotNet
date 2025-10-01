using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Library.Api.Controllers;
using Library.Application.Common.Paged;
using Library.Application.MediatR.Books.Queries.GetAll;
using Library.Application.MediatR.Books.Commands.Create;
using Library.Application.MediatR.Books.Commands.Delete;
using Library.Application.MediatR.Books.Commands.Update;

namespace Library.UnitTests.Api.Controllers;

public class BookControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly BookController _controller;

    public BookControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new BookController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithPagedResult()
    {
        // Arrange
        var query = new BooksGetAllQuery();
        var pagedResult = new PagedResult<BooksGetAllResult>
        {
            Items = new List<BooksGetAllResult>
                {
                    new BooksGetAllResult { Id = Guid.NewGuid(), Name = "Book 1", Author = "Author 1", Summary = "Summary 1" }
                },
            TotalCount = 1,
            Page = 1,
            PageSize = 50
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<BooksGetAllQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(pagedResult);

        // Act
        var result = await _controller.GetAll(query);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsType<PagedResult<BooksGetAllResult>>(okResult.Value);
        Assert.Single(value.Items);
        Assert.Equal(1, value.TotalCount);
    }

    [Fact]
    public async Task Create_ReturnsCreated_WithBookCreateResult()
    {
        // Arrange
        var command = new BookCreateCommand { Name = "Book 1", Author = "Author 1", Summary = "Summary 1" };
        var createResult = new BookCreateResult(Guid.NewGuid());

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<BookCreateCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createResult);

        // Act
        var result = await _controller.Create(command);

        // Assert
        var createdAtAction = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.Create), createdAtAction.ActionName);
        var value = Assert.IsType<BookCreateResult>(createdAtAction.Value);
        Assert.Equal(createResult.Id, value.Id);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenBookUpdated()
    {
        // Arrange
        var command = new BookUpdateCommand { Id = Guid.NewGuid().ToString(), Name = "Updated Book", Author = "Updated Author", Summary = "Updated Summary" };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<BookUpdateCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.Update(command);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenBookDeleted()
    {
        // Arrange
        var bookId = Guid.NewGuid().ToString();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<BookDeleteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.Delete(bookId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}