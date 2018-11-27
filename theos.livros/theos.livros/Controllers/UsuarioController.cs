using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using theos.livros.Business;
using theos.livros.Entitys;

namespace theos.livros.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioBusiness _usuarioBusiness = new UsuarioBusiness();

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            try
            {
                var _livros = await Task.Run(() => _usuarioBusiness.ConsultarUsuario(id));

                return _livros;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
