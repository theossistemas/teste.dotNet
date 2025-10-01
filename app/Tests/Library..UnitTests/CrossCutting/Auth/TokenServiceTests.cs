using FluentAssertions;
using Library.CrossCutting.Auth;
using Library.CrossCutting.Auth.Models;
using Library.Shared.AppSettings;
using Library.Shared.AppSettings.Types;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Library.UnitTests.CrossCutting.Auth;

public class TokenServiceTests
{
    [Fact]
    public void ResponseAuth_ShouldReturnTokenModelWithToken()
    {
        // Arrange
        var tokenService = new TokenService();
        var login = new LoginModel
        {
            Email = "teste@exemplo.com",
            Role = "Admin"
        };

        // Act
        TokenModel result = tokenService.ResponseAuth(login);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Token_ShouldBeValidJwtWithClaims()
    {
        // Arrange
        var tokenService = new TokenService();
        var login = new LoginModel
        {
            Email = "teste@exemplo.com",
            Role = "Admin"
        };

        // Act
        TokenModel result = tokenService.ResponseAuth(login);

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(result.Token);

        // Assert
        jwtToken.Should().NotBeNull();
        jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value
            .Should().Be(login.Email);
        jwtToken.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value
            .Should().Be(login.Role);
    }

    [Fact]
    public void Token_ShouldHaveExpirationSet()
    {
        // Arrange
        var tokenService = new TokenService();
        var login = new LoginModel
        {
            Email = "teste@exemplo.com",
            Role = "User"
        };

        // Act
        TokenModel result = tokenService.ResponseAuth(login);

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(result.Token);

        // Assert
        jwtToken.ValidTo.Should().BeCloseTo((new DateTime()).AddHours(CustomConfiguration.JWTSettings.ExpireHours), precision: TimeSpan.FromHours(2));
    }
}