using Library.Application.MediatR.Books.Commands.Create;

namespace Library.UnitTests.Application.MediatR.Commands.Books.Commands.Create;

public class BookCreateResultTests
{
    [Fact]
    public void Constructor_ShouldAssignId()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = new BookCreateResult(id);

        // Assert
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public void Records_ShouldBeEqual_WhenIdsAreEqual()
    {
        // Arrange
        var id = Guid.NewGuid();
        var result1 = new BookCreateResult(id);
        var result2 = new BookCreateResult(id);

        // Act & Assert
        Assert.Equal(result1, result2); // record equality based on property values
    }

    [Fact]
    public void Records_ShouldNotBeEqual_WhenIdsAreDifferent()
    {
        // Arrange
        var result1 = new BookCreateResult(Guid.NewGuid());
        var result2 = new BookCreateResult(Guid.NewGuid());

        // Act & Assert
        Assert.NotEqual(result1, result2);
    }
}