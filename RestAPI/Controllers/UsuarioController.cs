using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Usuarios;
using System;
using System.Text;

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

        [AllowAnonymous]
        [HttpPost("{login}/{senha}")]
        public Boolean ValidarLogin(String login, String senha)
        {
            senha = Convert.ToBase64String(Encoding.UTF8.GetBytes(senha));

            return usuarioService.ValidarLogin(login, senha);
        }
    }
}
