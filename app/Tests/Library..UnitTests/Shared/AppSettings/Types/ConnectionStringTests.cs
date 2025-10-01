using FluentAssertions;

namespace Library.UnitTests.Shared.AppSettings.Types;

public class ConnectionStringsTypeTests
{
    [Fact]
    public void DefaultConnection_ShouldSetAndGetValue()
    {
        // Arrange
        var connectionStrings = new ConnectionStringsType();
        string expectedValue = "Server=.;Database=TestDb;";

        // Act
        connectionStrings.DefaultConnection = expectedValue;

        // Assert
        connectionStrings.DefaultConnection.Should().Be(expectedValue);
    }
}