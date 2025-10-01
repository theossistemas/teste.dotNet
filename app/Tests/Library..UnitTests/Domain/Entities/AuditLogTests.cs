using FluentAssertions;
using Library.Domain.Entities;

namespace Library.UnitTests.Domain.Entities;
public class AuditLogTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesWithDefaults()
    {
        // Arrange & Act
        var auditLog = new AuditLog();

        // Assert
        auditLog.TableName.Should().BeNullOrEmpty();
        auditLog.EventType.Should().BeNullOrEmpty();
        auditLog.UserName.Should().BeNull();
        auditLog.Data.Should().BeNullOrEmpty();
    }

    [Fact]
    public void ShouldAllowSettingAndGettingProperties()
    {
        // Arrange
        var auditLog = new AuditLog();

        // Act
        auditLog.TableName = "Books";
        auditLog.EventType = "INSERT";
        auditLog.UserName = "johndoe";
        auditLog.Data = "{ \"Name\": \"Clean Code\" }";

        // Assert
        auditLog.TableName.Should().Be("Books");
        auditLog.EventType.Should().Be("INSERT");
        auditLog.UserName.Should().Be("johndoe");
        auditLog.Data.Should().Be("{ \"Name\": \"Clean Code\" }");
    }
}