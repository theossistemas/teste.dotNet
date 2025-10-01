using FluentAssertions;
using Library.Application.Common.Paged;
using Library.Application.MediatR.Books.Queries.GetAll;
using MediatR;

namespace Library.UnitTests.Application.MediatR.Queries.GetAll;

public class BooksGetAllQueryTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var query = new BooksGetAllQuery();

        // Assert
        query.Name.Should().BeNull();
        query.Page.Should().Be(1);      // assumindo que GetPaged define Page = 1 por padrão
        query.PageSaze.Should().Be(50); // assumindo que GetPaged define PageSaze = 50 por padrão
    }

    [Fact]
    public void ShouldImplementIRequestInterface()
    {
        // Arrange
        var query = new BooksGetAllQuery();

        // Act & Assert
        query.Should().BeAssignableTo<IRequest<PagedResult<BooksGetAllResult>>>();
    }

    [Fact]
    public void ShouldAllowSettingProperties()
    {
        // Arrange
        var query = new BooksGetAllQuery
        {
            Name = "Clean Code",
            Page = 2,
            PageSaze = 5
        };

        // Act & Assert
        query.Name.Should().Be("Clean Code");
        query.Page.Should().Be(2);
        query.PageSaze.Should().Be(5);
    }
}