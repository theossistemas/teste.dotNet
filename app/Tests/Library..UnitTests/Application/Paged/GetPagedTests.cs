using Library.Application.Common.Paged;

namespace Library.UnitTests.Application.Paged;

public class GetPagedTests
{
    [Fact]
    public void DefaultValues_ShouldBeCorrect()
    {
        // Arrange & Act
        var paged = new GetPaged();

        // Assert
        Assert.Equal(1, paged.Page);
        Assert.Equal(50, paged.PageSaze); // Repare que há um typo, talvez queira PageSize
        Assert.Equal("desc", paged.OrderBy);
    }

    [Fact]
    public void CanSetProperties()
    {
        // Arrange
        var paged = new GetPaged();

        // Act
        paged.Page = 5;
        paged.PageSaze = 100; // ou PageSize
        paged.OrderBy = "asc";

        // Assert
        Assert.Equal(5, paged.Page);
        Assert.Equal(100, paged.PageSaze);
        Assert.Equal("asc", paged.OrderBy);
    }
}