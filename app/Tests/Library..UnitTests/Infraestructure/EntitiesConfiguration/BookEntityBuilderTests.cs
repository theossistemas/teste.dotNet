using FluentAssertions;
using Library.Domain.Entities;
using Library.Infraestructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Library.UnitTests.Infraestructure.EntitiesConfiguration;

public class BookEntityBuilderTests
{
    [Fact]
    public void Configure_ShouldMapPropertiesCorrectly()
    {
        // Arrange
        var modelBuilder = new ModelBuilder(new ConventionSet());
        var builder = modelBuilder.Entity<Book>();
        var config = new BookEntityBuilder();

        // Act
        config.Configure(builder);
        var entityType = builder.Metadata;

        // Assert
        entityType.GetTableName()!.Should().Be("BOOKS");
        entityType.FindPrimaryKey()!.Properties.Select(p => p.Name).Should().ContainSingle("Id");

        entityType.FindProperty("Id")!.GetColumnName().Should().Be("ID");
        entityType.FindProperty("Name")!.GetColumnName().Should().Be("NAME");
        entityType.FindProperty("Name")!.GetMaxLength().Should().Be(200);
        entityType.FindProperty("Author")!.GetColumnName().Should().Be("AUTHOR");
        entityType.FindProperty("Author")!.GetMaxLength().Should().Be(150);
        entityType.FindProperty("Summary")!.GetColumnName().Should().Be("SUMMARY");
        entityType.FindProperty("Summary")!.GetMaxLength().Should().Be(1000);
        entityType.FindProperty("Active")!.GetColumnName().Should().Be("ACTIVE");
        entityType.FindProperty("CreatedAt")!.GetColumnName().Should().Be("CREATED_AT");

        // Verifica índice único no Name
        var nameIndex = entityType.GetIndexes().FirstOrDefault(i => i.Properties.Any(p => p.Name == "Name"));
        nameIndex.Should().NotBeNull();
        nameIndex.IsUnique.Should().BeTrue();
    }
}