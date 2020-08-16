using Microsoft.AspNetCore.Mvc;
using Services.Usuarios;
using System;

namespace RestAPI.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        /// <summary>
        /// Método para validação de login
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns>Verdadeiro / Falso</returns>
        [HttpPost("{login}/{senha}")]
        public Boolean ValidarLogin(String login, String senha)
        {
            return usuarioService.ValidarLogin(login, senha);
        }
    }
}
