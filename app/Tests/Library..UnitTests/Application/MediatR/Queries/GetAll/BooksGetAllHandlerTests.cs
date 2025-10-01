using FluentAssertions;
using Library.Application.MediatR.Books.Queries.GetAll;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Moq;

namespace Library.UnitTests.Application.MediatR.Queries.GetAll;

public class BooksGetAllHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly BooksGetAllHandler _handler;

    public BooksGetAllHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new BooksGetAllHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnActiveBooks_WhenNameIsEmpty()
    {
        // Arrange
        var query = new BooksGetAllQuery
        {
            Name = null,
            Page = 1,
            PageSaze = 10,
            OrderBy = "asc"
        };

        var books = new List<Book>
        {
            Book.Create("Clean Code", "Robert C. Martin", "Código limpo") ,
            Book.Create("DDD", "Eric Evans", "Domain Driven Design")
        };

        _bookRepositoryMock
            .Setup(r => r.GetBookOrderByName(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>(),
                                             true, query.Page.Value, query.PageSaze.Value, query.OrderBy, It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);

        _bookRepositoryMock
            .Setup(r => r.Count(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(books.Count);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(2);
        result.Items.Select(x => x.Name).Should().Contain(new[] { "Clean Code", "DDD" });
    }

    [Fact]
    public async Task Handle_ShouldFilterBooks_WhenNameIsProvided()
    {
        // Arrange
        var query = new BooksGetAllQuery
        {
            Name = "Clean",
            Page = 1,
            PageSaze = 10,
            OrderBy = "asc"
        };

        var books = new List<Book>
        {
            Book.Create("Clean Code", "Robert C. Martin", "Código limpo") 
        };

        _bookRepositoryMock
            .Setup(r => r.GetBookOrderByName(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>(),
                                             true, query.Page.Value, query.PageSaze.Value, query.OrderBy, It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);

        _bookRepositoryMock
            .Setup(r => r.Count(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Items.Should().HaveCount(1);
        result.TotalCount.Should().Be(1);
        result.Items.First().Name.Should().Be("Clean Code");
    }

    [Fact]
    public async Task Handle_ShouldReturnEmpty_WhenNoBooksFound()
    {
        // Arrange
        var query = new BooksGetAllQuery
        {
            Name = "Inexistente",
            Page = 1,
            PageSaze = 10,
            OrderBy = "asc"
        };

        _bookRepositoryMock
            .Setup(r => r.GetBookOrderByName(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>(),
                                             true, query.Page.Value, query.PageSaze.Value, query.OrderBy, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Book>());

        _bookRepositoryMock
            .Setup(r => r.Count(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
    }
}