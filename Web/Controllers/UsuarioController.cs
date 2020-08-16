using Microsoft.AspNetCore.Mvc;
using Services.Usuarios;
using System;

namespace Web.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("{login}/{senha}")]
        public Boolean ValidarLogin(String login, String senha)
        {
            return usuarioService.ValidarLogin(login, senha);
        }
    }
}
