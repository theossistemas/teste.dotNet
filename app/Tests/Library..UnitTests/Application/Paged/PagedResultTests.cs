using Library.Application.Common.Paged;

namespace Library.UnitTests.Application.Paged;

public class PagedResultTests
{
    [Fact]
    public void DefaultConstructor_ShouldInitializeEmptyItems()
    {
        // Arrange & Act
        var result = new PagedResult<string>();
        result.TotalCount = 10;
        result.PageSize = 2;

        // Assert
        Assert.NotNull(result.Items);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.Page);
        Assert.Equal(2, result.PageSize);
        Assert.Equal(10, result.TotalCount);
        Assert.Equal(5, result.TotalPages);
    }

    [Fact]
    public void Constructor_WithValues_ShouldSetProperties()
    {
        // Arrange
        var items = new List<string> { "Item1", "Item2", "Item3" };
        int totalCount = 10;
        int page = 2;
        int pageSize = 3;

        // Act
        var result = new PagedResult<string>(items, totalCount, page, pageSize);

        // Assert
        Assert.Equal(items, result.Items);
        Assert.Equal(totalCount, result.TotalCount);
        Assert.Equal(page, result.Page);
        Assert.Equal(pageSize, result.PageSize);
    }

    [Theory]
    [InlineData(10, 3, 4)] // 10 items, pageSize 3 => 4 pages
    [InlineData(0, 5, 0)]  // 0 items, pageSize 5 => 0 pages
    [InlineData(7, 7, 1)]  // 7 items, pageSize 7 => 1 page
    [InlineData(5, 10, 1)] // 5 items, pageSize 10 => 1 page
    public void TotalPages_ShouldCalculateCorrectly(int totalCount, int pageSize, int expectedTotalPages)
    {
        // Arrange
        var result = new PagedResult<string>
        {
            TotalCount = totalCount,
            PageSize = pageSize
        };

        // Act
        var totalPages = result.TotalPages;

        // Assert
        Assert.Equal(expectedTotalPages, totalPages);
    }
}