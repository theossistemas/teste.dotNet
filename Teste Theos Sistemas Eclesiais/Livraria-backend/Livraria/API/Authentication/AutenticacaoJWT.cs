using Livraria.Domain.Interfaces;
using Livraria.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Livraria.Api.Authentication
{
    public class AutenticacaoJWT : IAutenticacaoJWT
    {
        private readonly IConfiguration _configuration;

        public AutenticacaoJWT(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GerarTokenAsync(string username, string password, string role)
        {
            // Usuário fixo para teste
            if (username != "admin" || password != "1234" || role != "Admin")
                throw new AutenticacaoJWTException();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var secret = _configuration["Jwt:Secret"];
            if (string.IsNullOrWhiteSpace(secret) || secret.Length < 32)
                throw new InvalidOperationException("JWT Secret inválido ou menor que 32 caracteres.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );
            var tokenGerado = Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
            Log.Information("Token gerado com sucesso para o Usuario {username}", username);
            return tokenGerado;
        }
    }
}
