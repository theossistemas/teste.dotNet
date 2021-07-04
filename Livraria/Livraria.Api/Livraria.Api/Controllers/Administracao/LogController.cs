using AutoMapper;
using Livraria.Api.Dto;
using Livraria.Domain.Dto.Administracao;
using Livraria.Domain.Entities.Administracao;
using Livraria.Services.Interfaces.Administracao;
using Livraria.Util.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Api.Controllers.Administracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerApiBase
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public LogController(ILogService logService, IMapper mapper, ILogger<LogController> logger) : base(logger)
        {
            _logService = logService;
            _mapper = mapper;
        }

        [HttpGet("ConsultarTodos")]
        public async Task<ActionResult<LogDto>> ConsultarTodos()
        {
            try
            {
                var logs = await _logService.ConsultarTodos();
                var logsDto = _mapper.Map<IList<Log>, IList<LogDto>>(logs);

                return Ok(logsDto);
            }
            catch (AggregateException aex)
            {
                LogarErro($"Erro ao tentar consultar todos os logs: {aex.Message} {aex.InnerExceptions.MontarMensagemErro()}");
                return BadRequest(new ErroResponse(aex.InnerExceptions));
            }

            catch (Exception ex)
            {
                LogarErro($"Erro ao tentar consultar todos os logs: {ex.Message} {ex.InnerException}");
                return BadRequest(new ErroResponse(ex));
            }
        }
    }
}
