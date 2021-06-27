using System.Linq;
using System.Net.Http.Headers;
using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dto;
using Api.Domain.Dto.User;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IAdminService _serviceAdmin;

        private readonly ILogErrorService _serviceLog;

        public UserController(IUserService service, IAdminService serviceAdmin, ILogErrorService serviceLog)
        {
            _service = service;
            _serviceAdmin = serviceAdmin;
            _serviceLog = serviceLog;
        }

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                return Ok(await _service.Get(id));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDtoCreate user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _service.Post(user);

            try
            {
                if (result != null)
                {
                    return CreatedAtAction(nameof(Get), new { id = result.Id }, result);

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                var logDto = new LogErrorDto
                {
                    CreatedAt = DateTime.UtcNow,
                    Message = e.Message,
                    User = result.Id,
                    NameUser = result.Name

                };
                await _serviceLog.AddError(logDto);

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserDtoUpdate user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _service.Put(user);

            try
            {
                if (result != null)
                {
                    return Ok(result);

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                var logDto = new LogErrorDto
                {
                    CreatedAt = DateTime.UtcNow,
                    Message = e.Message,
                    NameUser = result.Name
                };
                await _serviceLog.AddError(logDto);

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, Guid idUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var admin = await _serviceAdmin.IsAdmin(idUser);
                if (!admin)
                {
                    return StatusCode((int)HttpStatusCode.Unauthorized);
                }
                return Ok(await _service.Delete(id));
            }
            catch (Exception e)
            {
                var logDto = new LogErrorDto
                {
                    CreatedAt = DateTime.UtcNow,
                    Message = e.Message,
                    User = idUser
                };
                await _serviceLog.AddError(logDto);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
