using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace Architecture
{
    public static class HttpExtensions
    {
        public static Guid AccountID(this HttpContext context, ApplicationSettings jwtSettings)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var key = Encoding.ASCII.GetBytes(jwtSettings.JWTSecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = DateTime.Now.AddHours(11).TimeOfDay
                }, out validatedToken);
            }
            catch (Exception e)
            {
                throw new TokenExperidedException(string.Format("Token expired {0}!", e.Message));
            }

            var jwtToken = (JwtSecurityToken)validatedToken;
            return Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
        }
    }
}
