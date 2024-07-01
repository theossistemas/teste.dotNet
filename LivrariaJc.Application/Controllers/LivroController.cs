using LivrariaJc.Domain.Imput;
using LivrariaJc.Domain.Input;
using LivrariaJc.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LivrariaJc.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/livro")]
    public class LivroController : ControllerBase
    {
        public readonly ILivroServices _services;
        private readonly ILogger _logger;


        public LivroController(ILivroServices services, ILogger logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get([FromQuery]LivroFilterInput input)
        {
            try
            {
                var resultado = await _services.ObterTodosAsync(input);

                return resultado.Error.Any() ? BadRequest(resultado.Error) : Ok(resultado.Data);
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro ocorreu ao solicitar requisição GET: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro Interno no Servidor.");
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var resultado = await _services.ObterAsync(id);

                return resultado.Error.Any() ? BadRequest(resultado.Error) : Ok(resultado.Data);
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro ocorreu ao solicitar requisição GET: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro Interno no Servidor.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(LivroPostDto dto)
        {
            try
            {
                var resultado = await _services.NovoAsync(dto);

                return resultado.Error.Any() ? BadRequest(resultado.Error) : Created();
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro ocorreu ao solicitar requisição POST: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro Interno no Servidor.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(LivroPutDto dto)
        {
            try
            {
                var resultado = await _services.AlterarAsync(dto);

                return resultado.Error.Any() ? BadRequest(resultado.Error) : Ok(resultado.Data);
            }

            catch (Exception ex)
            {
                _logger.Error($"Erro ocorreu ao solicitar requisição PUT: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro Interno no Servidor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _services.ExcluirAsync(id);

                return resultado.Error.Any() ? BadRequest(resultado.Error) : Ok(resultado.Data);
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro ocorreu ao solicitar requisição DELETE: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro Interno no Servidor.");
            }
        }
    }
}
