using Domain.Interface;
using Domain.Model.User;
using LoggerService.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService { get; }
        private ILoggerManager _logger { get; }
        public UserController(IUserService userService, ILoggerManager logger)
        {
            _userService = userService;
            _logger = logger;
        }


        /// <summary>
        /// Método de autenticação de usuário - Acesso Livre
        /// </summary>
        /// <remarks>
        /// Exemplo:
        /// 
        ///     POST
        ///     {
        ///         "username": "Admin",
        ///         "password": "admin123"
        ///     }
        /// </remarks>
        /// <param name="user">Credenciais do usuário a logar.</param>
        /// <returns>Um token de autenticação.</returns>
        /// <response code="200">Token de autenticação.</response>
        /// <response code="400">Usuário ou senha inválido.</response>
        /// <response code="500">Exceção não tratada.</response>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserModel user)
        {
            try
            {
                var token = _userService.Authenticate(user);

                if (token == null)
                    return BadRequest("Usuário ou senha inválido.");

                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro não tratado na classe UserController - método Login: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao consultar dados.");
            }
        }
    }
}
