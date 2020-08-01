using AutoMapper;
using Base.Domain.Entities.Cadastros.Base;
using Domain.Interfaces.Services.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Domain.Entities.UsuarioModel;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginController> _logger;
        public LoginController(SignInManager<ApplicationUser> signInManager,
                               UserManager<ApplicationUser> userManager,
                               IUsuarioService usuarioService,
                               IMapper mapper,
                               ILogger<LoginController> logger)
        {

            _signInManager = signInManager;
            _userManager = userManager;
            _usuarioService = usuarioService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUsuario model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Senha, false, true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user.Ativo)
                {
                    var claimsIdentity = await _userManager.GetClaimsAsync(user);
                    var usuarioClaim = new List<Claim>(new[]
                    {
                        new Claim("UsuarioId", user.Id.ToString()),
                        new Claim("UsuarioLogin", user.UserName),
                        new Claim("UsuarioNome", user.Nome),
                        new Claim("UsuarioSobrenome", user.Sobrenome),
                        new Claim("UsuarioTelefone", user.Telefone),
                        new Claim("UsuarioLogin", user.UserName),
                    });
                    _logger.LogWarning($"Usuario {user.Nome} Logado");
                    return Ok(new LoginUsuarioDto { Jwt = _usuarioService.GerarJwt(claimsIdentity, user.Email, usuarioClaim) });
                }
                else
                {
                    _logger.LogWarning("Usuario não encontrado");
                    return BadRequest("Usuario não encontrado");
                }
            }
            else
            {
                _logger.LogWarning("Usuario ou Senha inválido");
                return BadRequest("Usuario ou Senha inválido");
            }
        }
    }
}