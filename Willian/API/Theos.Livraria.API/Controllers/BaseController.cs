using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using System;
using System.Threading.Tasks;
using Theos.Livraria.Domain.Model;

namespace Theos.Livraria.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    { 
        protected readonly ILogger _logger;  

        protected BaseController(ILogger logger )
        {
            _logger = logger; 
        }
         
        protected async Task<IActionResult> TratarResultadoAsync(Func<Task<IActionResult>> servico)
        {
            try
            {
                return await servico();
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao processar a solicitação: " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { Mensagem = "Ocorreu um erro ao processar a solicitação. Por favor, tente novamente mais tarde." });
            }
        }
    }
}