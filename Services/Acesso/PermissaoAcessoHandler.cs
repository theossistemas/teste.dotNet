using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models.DTO;
using Services.Usuarios;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Services.Acesso
{
    public class PermissaoAcessoHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IUsuarioService usuarioService;

        public PermissaoAcessoHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUsuarioService usuarioService)
             : base(options, logger, encoder, clock)
        {
            this.usuarioService = usuarioService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return await Task.Run(() => AuthenticateResult.Fail("Cabeçalho de autorização não informado!"));

            Boolean autenticado = false;

            String username = String.Empty;

            try
            {
                AuthenticationHeaderValue authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                Byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                String[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                username = credentials[0];
                String password = credentials[1];

                password = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

                autenticado = usuarioService.ValidarLogin(username, password);
            }
            catch
            {
                return AuthenticateResult.Fail("Cabeçalho de autorização inválido!");
            }

            if (!autenticado)
                return AuthenticateResult.Fail("Login e/ou senha inválidos!");

            UsuarioDTO usuario = usuarioService.FindUserByLogin(username);

            Claim[] claims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.GetValueOrDefault().ToString()),
                new Claim(ClaimTypes.Name, usuario.Login),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}