using LivrariaWeb.Models;
using LivrariaWeb.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // Credenciais predefinidas
        private readonly string UsernamePredefinido = "admin";
        private readonly string PasswordPredefinido = "123456";

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Username == UsernamePredefinido && login.Password == PasswordPredefinido)
            {
                var token = _authService.GenerateToken(login.Username, true);
                return Ok(new { Token = token });
            }

            return Unauthorized("Credenciais inválidas");
        }
    }
}
