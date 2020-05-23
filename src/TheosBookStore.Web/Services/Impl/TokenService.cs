using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TheosBookStore.Auth.App.Models;

namespace TheosBookStore.Web.Services.Impl
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(AuthenticatedUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = GetTokenDescriptor(user, time: 2);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor GetTokenDescriptor(AuthenticatedUser user, int time)
        {
            var secret = _configuration["JWT:Secret"];
            Console.WriteLine(secret);
            var key = Encoding.ASCII.GetBytes(secret);
            IList<Claim> claims = GetClaims(user);
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(time),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenDescriptor;
        }

        private IList<Claim> GetClaims(AuthenticatedUser user)
        {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Name));
            claims.Add(new Claim(ClaimTypes.Role, user.Roles));
            return claims;
        }
    }
}

