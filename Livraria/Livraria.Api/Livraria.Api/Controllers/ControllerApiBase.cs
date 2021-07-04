using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Livraria.Api.Controllers
{
    public class ControllerApiBase : ControllerBase
    {
        private readonly ILogger _logger;

        public ControllerApiBase(ILogger logger = null)
        {
            _logger = logger;
        }

        protected void LogarInformation(string mensagem)
        {
            _logger.LogInformation($"{mensagem} - Usuário: {GetNomeUsuarioLogado()}");
        }

        protected void LogarWarning(string mensagem)
        {
            _logger.LogWarning($"{mensagem} - Usuário: {GetNomeUsuarioLogado()}");
        }

        protected void LogarErro(string mensagem)
        {
            _logger.LogError($"{mensagem} - Usuário: {GetNomeUsuarioLogado()}");
        }

        protected int GetIdUsuarioLogado()
        {
            string valorEncontrado = GetSidUsuarioLogado();
            int.TryParse(valorEncontrado, out int idUsuario);
            return idUsuario;
        }

        private string GetSidUsuarioLogado()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirst(ClaimTypes.Sid)?.Value;
        }

        private string GetNomeUsuarioLogado()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
