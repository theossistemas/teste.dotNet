using Base.Domain.Entities.Cadastros.Base;
using Domain.Interfaces.Services.Usuario;
using Domain.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Domain.Entities.UsuarioModel;

namespace Application.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(SignInManager<ApplicationUser> signInManager,
                                 UserManager<ApplicationUser> userManager,
                                 IOptions<TokenConfigurations> tokenConfigurations,
                                 IUsuarioService usuarioService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _usuarioService = usuarioService;
        }

        [HttpPost("NovaConta")]
        public async Task<ActionResult> Registrar(RegistrarUsuario model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            try
            {
                var user = new ApplicationUser
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    Telefone = model.Telefone,                    
                    EmailConfirmed = true,
                    UserName = model.UserName,
                    Ativo = true,
                    Sobrenome = model.Sobrenome
                };

                var result = await _userManager.CreateAsync(user, model.Senha);

                if (!result.Succeeded) return BadRequest(result.Errors);



                var usuario = await _userManager.FindByNameAsync(model.UserName);
                return Ok(new UsuarioGet
                {
                    Email = usuario.Email,
                    Id = usuario.Id.ToString(),
                    Nome = usuario.Nome,                    
                    Telefone = usuario.Telefone,
                    UserName = usuario.UserName,
                    Ativo = usuario.Ativo,
                    Sobrenome = usuario.Sobrenome
                });

            }
            catch (Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut("AtualizarConta")]
        public async Task<ActionResult> AtualizarConta(AtualizarUsuario model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(model.Senha))
                    {
                        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Senha);
                    }

                    user.Nome = model.Nome;
                    user.Email = model.Email;
                    user.Telefone = model.Telefone;                    
                    user.EmailConfirmed = true;
                    user.UserName = model.UserName;
                    user.Ativo = true;
                    user.Sobrenome = model.Sobrenome;

                    var result = await _userManager.UpdateAsync(user);

                    if (!result.Succeeded) return BadRequest(result.Errors);

                    var claims = await _userManager.GetClaimsAsync(user);
                    await _userManager.RemoveClaimsAsync(user, claims);

                    return Ok(new UsuarioGet
                    {
                        Email = user.Email,
                        Id = user.Id.ToString(),
                        Nome = user.Nome,                        
                        Telefone = user.Telefone,
                        UserName = user.UserName,
                        Ativo = user.Ativo
                    });

                }

                return BadRequest("Usuario não encontrado");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
            }

            try
            {
                var res = await _userManager.Users.ToListAsync();
                var usuario = res.Select(x => new UsuarioGet
                {
                    Email = x.Email,

                    Id = x.Id.ToString(),
                    Nome = x.Nome,                    
                    Telefone = x.Telefone,
                    UserName = x.UserName,
                    Ativo = x.Ativo,
                    Sobrenome = x.Sobrenome
                });

                return Ok(usuario);

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetUsuarioId")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var res = await _userManager.FindByIdAsync(id.ToString());

                if (res != null)
                {
                    var usuario = new UsuarioGet
                    {
                        Email = res.Email,
                        Id = res.Id.ToString(),
                        Nome = res.Nome,                        
                        Telefone = res.Telefone,
                        UserName = res.UserName,
                        Ativo = res.Ativo,
                        Sobrenome = res.Sobrenome
                    };
                    return Ok(usuario);
                }

                return BadRequest("Usuario não encontrado");
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

      
        [HttpDelete]
        public async Task<IActionResult> DesativarConta(int Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id.ToString());
                if (user != null)
                {
                    user.Ativo = false;

                    var result = await _userManager.UpdateAsync(user);

                    if (!result.Succeeded) return BadRequest(result.Errors);

                    return Ok("Usuario Desativado Com Sucesso");
                }

                return BadRequest("Usuario não encontrado");
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut("AtivarConta")]
        public async Task<IActionResult> AtivarConta(int Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id.ToString());
                if (user != null)
                {
                    user.Ativo = true;

                    var result = await _userManager.UpdateAsync(user);

                    if (!result.Succeeded) return BadRequest(result.Errors);

                    return Ok("Usuario Ativado Com Sucesso");
                }

                return BadRequest("Usuario não encontrado");

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }       
    }
}