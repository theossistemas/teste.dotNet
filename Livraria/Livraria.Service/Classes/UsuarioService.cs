using Livraria.Domain.Interface.Repositories;
using Livraria.Service.DTOs;
using Livraria.Service.Interfaces;
using Livraria.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Livraria.Service.Classes
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AppSettings _appSettings;
        public UsuarioService(IUsuarioRepository usuarioRepository, IOptions<AppSettings> appSettings)
        {
            _usuarioRepository = usuarioRepository;
            _appSettings = appSettings.Value;
        }
        public UsuarioDTO Autenticar(string login, string senha)
        {
            var usuario = _usuarioRepository.Autenticar(login, senha);
            if (usuario == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new UsuarioDTO()
            {
                Id = usuario.Id.ToString(),
                Login = usuario.Login,
                Token = tokenHandler.WriteToken(token),
            };
        }
    }
}
