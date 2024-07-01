using LivrariaJc.Domain.Input;
using LivrariaJc.Domain.Interfaces.Services;
using LivrariaJc.Domain.Output;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace LivrariaJc.Service.Services
{
    public class AutenticarServices : IAutenticarServices
    {
        private readonly IConfiguration _configuration;

        public AutenticarServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ServiceResult Login(LoginInput input)
        {
            var validacoes = ValidaLogin(input);

            if (validacoes.Error.Count > 0)
                return new ServiceResult(validacoes.Error);

            return new ServiceResult(GerarToken(input));
        }

        private ServiceResult ValidaLogin(LoginInput input)
        {
            var validacoes = LoginInput.Validar(input);

            if (!validacoes.IsValid)
            {
                var resultado = new ServiceResult(null);
                validacoes.Errors.ToList().ForEach(e => resultado.AdicionarErro(e.PropertyName, e.ErrorMessage));
                return resultado;
            }

            if(!ValidaAcesso(input))
                return new ServiceResult("Login", "Usuario ou senha invalida!.");

            return new ServiceResult(true);
        }

        private bool ValidaAcesso(LoginInput input)
        {
            string usuario = _configuration.GetSection($"Users:Username").Value;
            string senha = _configuration.GetSection($"Users:Password").Value;

            if (usuario.ToLower().Trim() == input.UserName.ToLower().Trim() && senha == input.Password)
                return true;

            return false;
        }

        private string GerarToken(LoginInput input)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", input.UserName) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
