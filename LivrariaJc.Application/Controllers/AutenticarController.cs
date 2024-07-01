using Microsoft.AspNetCore.Mvc;
using System;
using LivrariaJc.Domain.Input;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using System.Net;
using LivrariaJc.Domain.Interfaces.Services;
using System.Linq;

namespace LivrariaJc.Application.Controllers
{
    [ApiController]
    [Route("api/v1/autenticar")]
    public class AutenticarController : ControllerBase
    {
        public readonly IAutenticarServices _services;
        private readonly ILogger _logger;

        public AutenticarController(IAutenticarServices services, ILogger logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Post(LoginInput input)
        {
            try
            {
                var resultado = _services.Login(input);

                return resultado.Error.Any() ? Unauthorized(resultado.Error) : Ok(resultado.Data);
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro ocorreu ao solicitar requisição POST: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro Interno no Servidor.");
            }
        }
    }
}
