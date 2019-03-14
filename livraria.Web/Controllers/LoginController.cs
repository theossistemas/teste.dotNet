using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using livraria.Application.interfaces;
using livraria.Domain.entities;
using livraria.Web.Mappers;
using livraria.Web.ModelToken;
using livraria.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace livraria.Web.Controllers
{
    [Route("api/login")]
    public class LoginController : Controller
    {

        [AllowAnonymous]
        [HttpPost]
        public object Post(
            [FromBody]UsuarioLogin usuarioLogin,
            [FromServices]IUsuarioApplication usuarioApplication,
            [FromServices]AuthConfig authConfig,
            [FromServices]TokenConfigurations tokenConfigurations)
        {

            bool credenciaisValidas = false;
            var usuario = usuarioLogin.MapTo<Usuario>();

            if (usuario.IsValidLogin)
            {
                var usuarioResult = usuarioApplication.Login(usuario);
                credenciaisValidas = usuarioResult != null;
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                 new GenericIdentity(usuario.Email, "Login"),
                 new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Email)
                 }
             );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = authConfig.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }

    }
}
