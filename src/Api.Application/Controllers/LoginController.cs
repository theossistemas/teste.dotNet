using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dto;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _service;

        private readonly ILogErrorService _serviceLog;

        public LoginController(ILoginService service, ILogErrorService serviceLog)
        {
            _service = service;
            _serviceLog = serviceLog;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto LoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (LoginDto == null)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.FindByLogin(LoginDto);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentException e)
            {
                var logDto = new LogErrorDto
                {
                    CreatedAt = DateTime.UtcNow,
                    Message = e.Message
                };
                await _serviceLog.AddError(logDto);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
