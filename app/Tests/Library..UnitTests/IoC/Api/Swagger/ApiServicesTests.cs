using Asp.Versioning;
using FluentAssertions;
using Library.IoC.Api.Swagger;
using Library.Shared.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace Library.UnitTests.IoC.Api.Swagger;

public class ApiServicesTests
{
    [Fact]
    public void AddApiServices_ShouldRegisterExpectedServices()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApiServices();
        services.AddLogging(); 
        var provider = services.BuildServiceProvider();

        // Act
        services.AddApiServices();
        // Assert
        // Verifica se GlobalExceptionHandling foi registrado
        var middleware = provider.GetService<GlobalExceptionHandling>();
        middleware.Should().NotBeNull();

        // Verifica se ApiVersioning foi registrado
        var apiVersioning = provider.GetService<IApiVersionReader>();
        apiVersioning.Should().NotBeNull();
    }
}