using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using System;
using System.Threading.Tasks;
using TheoLib.Dominio.Modelo;

namespace TheoLib.API.Controllers
{

    [ApiController]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        protected readonly ILogger _logger;

        public BaseController(ILogger logger )
        {
            _logger = logger; 
        }

        protected async Task<IActionResult> TratarResultado(Func<Task<IActionResult>> servico)
        {
            try
            {
                return await servico();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao processar a solicitação :::: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaBase { Mensagem = "Ocorreu um erro ao processar a solicitação. Por favor, tente novamente mais tarde." });
            }
        }

    }
}
