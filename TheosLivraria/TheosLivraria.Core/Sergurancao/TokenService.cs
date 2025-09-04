using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TheosLivraria.Domain.Entidades;

namespace TheosLivraria.Core.Sergurancao
{
    public class TokenService : ITokenService
    {

        private readonly string _secret = "e1d0a5e8-1d53-499f-b3d8-3468bb95e0b9";
        public string GerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Documento),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Perfil == Perfil.Administrador ? "Administrador" : "Publico")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               issuer: "Theos",
               audience: "TheosAPI",
               claims: claims,
               expires: DateTime.UtcNow.AddHours(7),
               signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
