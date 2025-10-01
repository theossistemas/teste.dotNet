using Audit.Core;
using Audit.Core.Providers;
using FluentAssertions;
using Library.Domain.Entities;
using Library.Infraestructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace Library.IntegrationTests.Infraestructure.AppDbContext;

public class DataContextTests
{
    private DataContext CreateInMemoryContext()
    {
        Configuration.Setup().UseNullProvider();
        Configuration.DataProvider = new NullDataProvider();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // banco isolado por teste
            .Options;

        return new DataContext(options);
    }

    [Fact]
    public void ShouldCreateDbSets()
    {
        // Arrange
        var context = CreateInMemoryContext();

        // Act & Assert
        context.Books.Should().NotBeNull();
        context.Users.Should().NotBeNull();
        context.AuditLog.Should().NotBeNull();
    }

    [Fact]
    public void OnModelCreating_ShouldApplyConfigurations()
    {
        // Arrange
        var context = CreateInMemoryContext();

        // Act
        var model = context.Model;

        // Assert
        model.GetEntityTypes().Any(e => e.ClrType == typeof(Book)).Should().BeTrue();
        model.GetEntityTypes().Any(e => e.ClrType == typeof(Login)).Should().BeTrue();
        model.GetEntityTypes().Any(e => e.ClrType == typeof(AuditLog)).Should().BeTrue();
    }
}