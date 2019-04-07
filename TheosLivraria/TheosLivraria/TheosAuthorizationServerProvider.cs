using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TheosLivraria
{
    public class TheosAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) 
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var usernamePublico = "publico";
            var senhaPublica = "senhaPublica";
            var usernameAdmin = "admin";
            var senhaAdmin = "admin";

            if (context.UserName == usernamePublico && context.Password == senhaPublica)
            {
                identity.AddClaims(new List<Claim>
                {
                    new Claim(ClaimTypes.Role, usernamePublico),
                    new Claim("username", usernamePublico),
                    new Claim(ClaimTypes.Name, "Convidado(a)")
                });
            }
            else if (context.UserName == usernameAdmin && context.Password == senhaAdmin)
            {
                identity.AddClaims(new List<Claim>
                {
                    new Claim(ClaimTypes.Role, usernameAdmin),
                    new Claim("username", usernameAdmin),
                    new Claim(ClaimTypes.Name, "Convidado(a)")
                });
            }
            else
            {
                context.SetError("Solicitação de permissão inválida", "Usuário e/ou senha não incorretos.");
                return;
            }
        }
    }
}
