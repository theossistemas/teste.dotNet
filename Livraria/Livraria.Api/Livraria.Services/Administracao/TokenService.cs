using Livraria.Domain.Entities.Administracao;
using Livraria.Services.Interfaces.Administracao;
using Livraria.Util.ExtensionMethods;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Livraria.Services.Administracao
{
    public class TokenService : ITokenService
    {
        private readonly IConfigurationManagerService _settingsService;

        public TokenService(IConfigurationManagerService settingsService)
        {
            _settingsService = settingsService;
        }

        public string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settingsService.GetSecurityKey());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _settingsService.GetSetting("AudienceKey"),
                Issuer = _settingsService.GetSetting("IssuerKey"),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role.ToEnumString())
                }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
