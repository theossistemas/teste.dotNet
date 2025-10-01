using FluentAssertions;
using Library.CrossCutting.Auth;
using Library.CrossCutting.Auth.Interface;
using Library.IoC.CrossCutting;
using Library.Shared.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Library.UnitTests.IoC.CrossCutting;

public class CrossCuttingIoCTests
{
    [Fact]
    public void AddCrossCutting_ShouldRegisterExpectedServices()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddCrossCutting();
        var provider = services.BuildServiceProvider();

        // Assert
        // Verifica se TokenService foi registrado
        var tokenService = provider.GetService<ITokenService>();
        tokenService.Should().NotBeNull();
        tokenService.Should().BeOfType<TokenService>();

        // Verifica se AuthenticationScheme JWT está registrado
        var schemes = provider.GetService<Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider>();
        schemes.Should().NotBeNull();

        // Verifica se a chave de assinatura do JWT está correta
        var key = Encoding.ASCII.GetBytes(CustomConfiguration.JWTSettings.Secret);
        key.Should().NotBeEmpty();
    }
}