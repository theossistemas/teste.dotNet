using AutoMapper;
using Domain.Interfaces.Services.Usuario;
using Domain.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Services.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly TokenConfigurations _tokenConfigurations;
        private readonly IMapper _mapper;
        public UsuarioService( IOptions<TokenConfigurations> tokenConfigurations, IMapper mapper)
        {

            _tokenConfigurations = tokenConfigurations.Value;

            _mapper = mapper;
        }
       
        public string GerarJwt(IList<Claim> claim, string email, List<Claim> claimUsuario)
        {
            return Jwt(claim, email, claimUsuario);
        }
        private string Jwt(IList<Claim> claim, string email, List<Claim> claimUsuario)
        {
            try
            {
                var claimsIdentity = claim;
                var identity = new ClaimsIdentity(
                  new GenericIdentity(email),
                  new[]
                  {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //jti O id do token
                    new Claim(JwtRegisteredClaimNames.UniqueName, email)
                  }
                );

                identity.AddClaims(claimsIdentity);
                identity.AddClaims(claimUsuario);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_tokenConfigurations.Secret);

                var securityToken = new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfigurations.Emissor,
                    Audience = _tokenConfigurations.ValidoEm,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Subject = identity,
                    Expires = DateTime.UtcNow.AddHours(_tokenConfigurations.ExpiracaoHoras),
                };

                return tokenHandler.WriteToken(tokenHandler.CreateToken(securityToken));
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }      
    }
}
