// public class AutenticacaoController
using Livraria.Service.Validators;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using livraria_api.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Livraria.Domain.Security.Interfaces;
using Livraria.Domain.Security.Models;
using livraria_api.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using Livraria.Domain.Security.Entities;
using BC = BCrypt.Net.BCrypt;
using Livraria.Infra.Data.Security.Authorization;

namespace livraria_api.Controllers
{
    [Route("livraria-api/[controller]")]
    
    public class AutenticacaoController : ControllerBase
    {

        private readonly ISecurityUserService _userService;
        private readonly IMapper _mapper;

        public AutenticacaoController(ISecurityUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginUserModels modelo)
        {
            if (modelo.Login == null || modelo.Password == null)
                return NotFound();

            var password = BC.HashPassword(modelo.Password);
            var usuario = _userService.GetLogin(modelo.Login);

            if (usuario == null || !BC.Verify(modelo.Password, usuario.Password))
            {
                return NotFound(new { message = "Usuário ou senha inválido" });
            }
            var token = TokenService.GenerateToken(usuario);

            var usuarioAutenticado = new UsuarioAutenticadoModels()
            {
                Login = usuario.Login,
                Name = usuario.Name,
                Role = usuario.Role,
                Token = token
            };
            return usuarioAutenticado;
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public IActionResult Create([FromBody] UsuarioModels usuarioModels)
        {
            if (usuarioModels == null)
                return NotFound("Informe um usuário");

            var usuario = _userService.GetLogin(usuarioModels.Login);
            if (usuario != null)
            {
                return Conflict("Login já cadastrado!");
            }

            var password = BC.HashPassword(usuarioModels.Password);
            usuarioModels.Password = password;

            var novoUsuario = _mapper.Map<User>(usuarioModels);

            var usuarioAdicionado = Execute(() => _userService.Insert(novoUsuario));

            return usuarioAdicionado;
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromBody] UsuarioModels usuarioModels)
        {

            if (usuarioModels == null)
                return NotFound("Informe um usuário");

            var usuario = _userService.Select(usuarioModels.Id);
            if (usuario == null)
            {
                return NotFound();
            }
            

            if (!string.IsNullOrEmpty(usuarioModels.Password))
            {
                var password = BC.HashPassword(usuarioModels.Password);
                usuarioModels.Password = password;
            }else {
                usuarioModels.Password = usuario.Password;
            }
            // if (!string.IsNullOrEmpty(usuarioModels.Login))
            //     usuarioNovo.Login = usuarioModels.Login;
            // if (!string.IsNullOrEmpty(usuarioModels.Name))
            //     usuarioNovo.Name = usuarioModels.Name;
            // if (!string.IsNullOrEmpty(usuarioModels.Role))
            //     usuarioNovo.Role = usuarioModels.Role;

            var novoUsuario = _mapper.Map<User>(usuarioModels);
            var usuarioAtualizado = Execute(() => _userService.Update(novoUsuario));
            return usuarioAtualizado;
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound("Usuário não informado");

            var usuario = _userService.Select(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado");
            }

            Execute(() =>
            {
                _userService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet("all")]
        [Authorize(Roles = Roles.Admin + "," + Roles.User)]
        public IActionResult GetAll()
        {
            return Execute(() => _userService.Select());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.User)]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound("Usuário não informado");

            return Execute(() => _userService.Select(id));
        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
