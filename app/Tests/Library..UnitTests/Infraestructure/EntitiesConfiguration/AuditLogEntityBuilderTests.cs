using FluentAssertions;
using Library.Domain.Entities;
using Library.Infraestructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Library.UnitTests.Infraestructure.EntitiesConfiguration;

public class AuditLogEntityBuilderTests
{
    [Fact]
    public void Configure_ShouldMapPropertiesCorrectly()
    {
        // Arrange
        var modelBuilder = new ModelBuilder(new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet());
        var builder = modelBuilder.Entity<AuditLog>();

        var config = new AuditLogEntityBuilder();

        // Act
        config.Configure(builder);

        var entityType = builder.Metadata;

        // Assert
        entityType.GetTableName().Should().Be("AUDIT_LOGS");
        entityType.FindPrimaryKey()!.Properties.Select(p => p.Name).Should().ContainSingle("Id");

        entityType.FindProperty("Id")!.GetColumnName().Should().Be("ID");
        entityType.FindProperty("TableName")!.GetColumnName().Should().Be("TABLE_NAME");
        entityType.FindProperty("EventType")!.GetColumnName().Should().Be("EVENT_TYPE");
        entityType.FindProperty("UserName")!.GetColumnName().Should().Be("USER_NAME");
        entityType.FindProperty("Data")!.GetColumnName().Should().Be("DATA");
        entityType.FindProperty("CreatedAt")!.GetColumnName().Should().Be("CREATED_AT");

        // Active foi ignorado
        entityType.FindProperty("Active").Should().BeNull();
    }
}