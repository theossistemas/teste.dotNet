using LivrariaWeb.Dto;
using LivrariaWeb.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public ITokenService _ITokenService;


        public TokenController(IConfiguration configuration, ITokenService ITokenService)
        {
            _configuration = configuration;
            _ITokenService = ITokenService;
        }

        // POST api/<TokenController>
        /// <summary>
        /// Retorna (JWT) Token de autenticação.
        /// </summary>
        /// <remarks>
        /// 
        ///  Retorna um token valido por 20 minutos caso as credencias de login estejam corretas.
        ///     Para utlizar este token basta adicionar ao Header (Authorize no Swagger), Bearer + Token segue o exemplo:
        ///    "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJJbnZlbnRvc..."
        /// </remarks>
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            if (email != "" && senha != "")
            {
                var dtoRequest = await _ITokenService.Login(email, senha);
                if (dtoRequest.Message == "Login ou Senha Invalido.") return BadRequest(new { Mensagem = "Login ou Senha Invalido." });
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                    new Claim("Id",dtoRequest.Result.Id.ToString()),
                    new Claim("UserName",dtoRequest.Result.Email),
                    new Claim("Password",dtoRequest.Result.Senha),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signIn);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Credenciais Invalidas.");
            }
        }

        // POST api/<TokenController>
        /// <summary>
        /// Cadastra um novo usuario.
        /// </summary>
        /// <remarks>
        /// 
        ///  Cadastra um novo usuario.
        /// </remarks>

        [HttpPost]
        [Route("Cadastrar")]
        public async Task<ActionResult> Cadastrar(string nome, string email, string senha)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid) return BadRequest(allErrors);

            var dtoPessoa = await _ITokenService.Cadastrar(nome, email, senha);
            return Ok(dtoPessoa);
        }
    }
}
