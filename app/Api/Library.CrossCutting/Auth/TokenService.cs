using Library.CrossCutting.Auth.Interface;
using Library.CrossCutting.Auth.Models;
using Library.Shared.AppSettings;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Library.CrossCutting.Auth;

public class TokenService : ITokenService
{
    private string CreateToken(LoginModel login)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(CustomConfiguration.JWTSettings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(ListClaims(login)),
            Expires = DateTime.UtcNow.AddHours(CustomConfiguration.JWTSettings.ExpireHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public TokenModel ResponseAuth(LoginModel login)
    {
        var response = new TokenModel
        {
            Token = CreateToken(login)
        };

        return response;
    }

    private IEnumerable<Claim> ListClaims(LoginModel login)
        => new[]
        {
                new Claim(JwtRegisteredClaimNames.Email, login.Email.ToString()),
                new Claim(ClaimTypes.Role, login.Role)
        };
}