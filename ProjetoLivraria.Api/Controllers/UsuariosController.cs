using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoLivraria.Api.Helpers;
using ProjetoLivraria.Application.Interfaces;
using ProjetoLivraria.Application.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace ProjetoLivraria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuariosController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(JwtSecurityTokenHandler))]
        [ProducesResponseType(400)]
        public IActionResult Authenticate([FromBody]LoginViewModel usuarioParam)
        {
            if (usuarioParam == null)
            {
                return BadRequest("Parâmetros inválidos.");
            }

            var usuario = _usuarioAppService.Authenticate(usuarioParam.Username, usuarioParam.Password);

            if (usuario == null)
            {
                return BadRequest(new { message = "Usuário ou senha incorretos" });
            }

            return Ok(new { usuario.Token });
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var usuarios = _usuarioAppService.GetAll();
            return Ok(usuarios);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult Getusuario(Guid id)
        {
            var usuario = _usuarioAppService.GetById(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var currentUsuarioId = Guid.Parse(User.Identity.Name);
            if (id != currentUsuarioId && !User.IsInRole(Role.Admin))
            {
                return Forbid();
            }

            return Ok(usuario);
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpPost("create")]
        public IActionResult Postusuario(UsuarioViewModel usuario)
        {
            usuario.Id = _usuarioAppService.Add(usuario).Id;
            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut]
        public IActionResult Putusuario(int id, UsuarioViewModel usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            try
            {
                usuario = _usuarioAppService.Update(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete]
        public IActionResult Deleteusuario(Guid id)
        {
            var usuario = _usuarioAppService.GetById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _usuarioAppService.Remove(id);

            return NoContent();

        }

        private bool UsuarioExists(int id)
        {
            return _usuarioAppService.GetAll().Any(t => t.Id == id);
        }
    }
}