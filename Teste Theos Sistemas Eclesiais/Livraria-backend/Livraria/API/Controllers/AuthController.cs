using Livraria.API.DTOs;
using Livraria.API.DTOs.Login;
using Livraria.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAutenticacaoJWT _authService;

        public AuthController(IAutenticacaoJWT authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var token = await _authService.GerarTokenAsync(login.Username, login.Password, login.TipoUsuario);
            return Ok(new { token });
        }
    }
}
