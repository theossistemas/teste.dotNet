using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace TheosTestAPI.Utilities
{
    public class CriptoAssistant
    {
        public string hideString(string str)
        {
            byte[] input = Encoding.ASCII.GetBytes(str);
            
            HashAlgorithm sha = SHA256.Create();

            byte[] hash = sha.ComputeHash(input);

            //https://stackoverflow.com/questions/27817282/sha256-giving-44-length-output-instead-64-length
            return BitConverter.ToString(hash).Replace("-", string.Empty); 
        }

        public string GenerateNewJWT(IConfiguration configuration)
        {
            //http://www.macoratti.net/19/06/aspnc_autjwt1.htm
            //http://www.macoratti.net/19/06/aspnc_jwtc1.htm
            DateTime expiry = DateTime.Now.AddMinutes(60);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(issuer: null, audience: null, expires: expiry, signingCredentials: credentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            string stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}
