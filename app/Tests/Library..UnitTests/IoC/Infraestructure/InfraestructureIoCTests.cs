using FluentAssertions;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infraestructure.AppDbContext;
using Library.Infraestructure.Repositories;
using Library.IoC.Infraestructure;
using Library.Shared.AppSettings;
using Microsoft.Extensions.DependencyInjection;

namespace Library.UnitTests.IoC.Infraestructure;

public class InfraestructureIoCTests
{
    [Fact]
    public void AddInfraestruucture_ShouldRegisterExpectedServices()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddLogging();

        // Act
        services.AddInfraestruucture();
        var provider = services.BuildServiceProvider();

        // Assert
        // Repositories genéricos
        var genericRepo = provider.GetService<IRepository<Login>>();
        genericRepo.Should().NotBeNull();
        genericRepo.Should().BeOfType<Repository<Login>>();

        // BookRepository específico
        var bookRepo = provider.GetService<IBookRepository>();
        bookRepo.Should().NotBeNull();
        bookRepo.Should().BeOfType<BookRepository>();

        // DbContext
        var context = provider.GetService<DataContext>();
        context.Should().NotBeNull();

        // HttpContextAccessor
        var httpContextAccessor = provider.GetService<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
        httpContextAccessor.Should().NotBeNull();
    }
}