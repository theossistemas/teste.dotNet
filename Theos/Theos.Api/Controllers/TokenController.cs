using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Theos.Model.Model;
using Theos.Service.Interface;

namespace Theos.Api.Controllers
{
    [ApiController]
    [Route("[controller]"),]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;
        public TokenController(IConfiguration configuration, IUsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken(string nome, string senha )
        {
            bool usuarioAutenticado = _usuarioService.UsuarioAutenticado(nome, senha);

            if (usuarioAutenticado == true)
            {
                var claims = new[]
                {
                     new Claim(ClaimTypes.Name, nome)
                };

                var key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));

               var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                     issuer: "sergio.martins",
                     audience: "sergio.martins",
                     claims: claims,
                     expires: DateTime.Now.AddMinutes(30),
                     signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return BadRequest("Credenciais inválidas...");
        }
    }
}
