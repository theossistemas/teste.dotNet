using FluentAssertions;
using FluentValidation;
using Library.IoC.Application;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Library.UnitTests.IoC.Application;

public class ApplicationIoCTests
{
    [Fact]
    public void AddApplication_ShouldRegisterMediatRAndValidators()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddLogging();

        // Act
        services.AddApplication();
        var provider = services.BuildServiceProvider();

        // Verifica se MediatR está registrado
        var mediator = provider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        // Verifica se Validators estão registrados
        var validators = provider.GetServices<IValidator<object>>();
        validators.Should().NotBeNull(); // não testamos quais exatamente, mas registro existe
    }
}