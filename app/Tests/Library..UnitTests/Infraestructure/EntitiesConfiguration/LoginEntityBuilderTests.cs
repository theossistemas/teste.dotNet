using FluentAssertions;
using Library.Domain.Entities;
using Library.Infraestructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Library.UnitTests.Infraestructure.EntitiesConfiguration;

public class LoginEntityBuilderTests
{
    [Fact]
    public void Configure_ShouldMapPropertiesCorrectly()
    {
        // Arrange
        var modelBuilder = new ModelBuilder(new ConventionSet());
        var builder = modelBuilder.Entity<Login>();
        var config = new LoginEntityBuilder();

        // Act
        config.Configure(builder);
        var entityType = builder.Metadata;

        // Assert
        entityType.GetTableName().Should().Be("Logins");
        entityType.FindPrimaryKey()!.Properties.Select(p => p.Name).Should().ContainSingle("Id");

        entityType.FindProperty("Id")!.GetColumnName().Should().Be("ID");
        entityType.FindProperty("UserName")!.GetColumnName().Should().Be("USER_NAME");
        entityType.FindProperty("Email")!.GetColumnName().Should().Be("EMAIL");
        entityType.FindProperty("PasswordHash")!.GetColumnName().Should().Be("PASSWORD_HASH");
        entityType.FindProperty("PasswordHash")!.GetMaxLength().Should().Be(60);
        entityType.FindProperty("Role")!.GetColumnName().Should().Be("ROLE");
        entityType.FindProperty("Active")!.GetColumnName().Should().Be("ACTIVE");
        entityType.FindProperty("CreatedAt")!.GetColumnName().Should().Be("CREATED_AT");

        // Propriedade ignorada (_email)
        entityType.FindProperty("_email").Should().BeNull();

        // Índices únicos
        var userNameIndex = entityType.GetIndexes().FirstOrDefault(i => i.Properties.Any(p => p.Name == "UserName"));
        userNameIndex.Should().NotBeNull();
        userNameIndex.IsUnique.Should().BeTrue();

        var emailIndex = entityType.GetIndexes().FirstOrDefault(i => i.Properties.Any(p => p.Name == "Email"));
        emailIndex.Should().NotBeNull();
        emailIndex.IsUnique.Should().BeTrue();
    }
}