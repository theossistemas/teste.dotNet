using FluentAssertions;
using Library.Application.MediatR.Books.Queries.GetAll;
using Library.Domain.Entities;

namespace Library.UnitTests.Application.MediatR.Queries.GetAll;

public class BooksGetAllMapperTests
{
    [Fact]
    public void MapToResult_ShouldMapCorrectly()
    {
        // Arrange
        var books = new List<Book>
        {
            Book.Create("Clean Code", "Robert C. Martin", "Um guia para código limpo"),
            Book.Create("DDD", "Eric Evans", "Domain Driven Design")
        };

        int totalCount = 2;
        int page = 1;
        int pageSize = 10;

        // Act
        var result = books.MapToResult(totalCount, page, pageSize);

        // Assert
        result.Should().NotBeNull();
        result.TotalCount.Should().Be(totalCount);
        result.Page.Should().Be(page);
        result.PageSize.Should().Be(pageSize);

        result.Items.Should().HaveCount(2);
        result.Items.Select(x => x.Name).Should().Contain(new[] { "Clean Code", "DDD" });
        result.Items.Select(x => x.Author).Should().Contain(new[] { "Robert C. Martin", "Eric Evans" });
    }

    [Fact]
    public void MapToResult_ShouldReturnEmpty_WhenListIsEmpty()
    {
        // Arrange
        var books = new List<Book>();
        int totalCount = 0;
        int page = 1;
        int pageSize = 10;

        // Act
        var result = books.MapToResult(totalCount, page, pageSize);

        // Assert
        result.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
        result.Page.Should().Be(page);
        result.PageSize.Should().Be(pageSize);
    }
}