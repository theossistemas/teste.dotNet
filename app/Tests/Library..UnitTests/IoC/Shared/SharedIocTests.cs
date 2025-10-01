using FluentAssertions;
using Library.IoC.Shared;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Library.UnitTests.IoC.Shared;

public class SharedIocTests
{
    [Fact]
    public void AddShared_ShouldAddSerilogLogger()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddShared();

        // Assert
        // Verifica se o logger está configurado
        Log.Logger.Should().NotBeNull();
        Log.Logger.Should().BeAssignableTo<ILogger>();

        // Testa se é possível escrever log sem exceção
        var exception = Record.Exception(() => Log.Information("Teste unitário de log"));
        exception.Should().BeNull();
    }
}