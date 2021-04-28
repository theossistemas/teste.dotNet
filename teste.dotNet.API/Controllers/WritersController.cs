using System;
using Microsoft.AspNetCore.Mvc;
using teste.dotNet.API.DTOs.Request;
using teste.dotNet.API.Services;

namespace teste.dotNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WritersController : ControllerBase
    {

        private WritersService _writerService;

        public WritersController(WritersService writerService)
        {
            _writerService = writerService;
        }

        [HttpGet]
        [Route("{writerId}")]
        public IActionResult Get(int writerId)
        {
            try
            {
                var writer = _writerService.Get(writerId);
                if (writer != null)
                    return Ok(writer);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public IActionResult List()
        {
            try
            {
                var writers = _writerService.List();
                if (writers != null && writers.Count > 0)
                    return Ok(writers);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add([FromBody] WriterRequestDTO writer)
        {
            try
            {
                var message = _writerService.Add(writer);
                if (string.IsNullOrEmpty(message))
                    return Ok("Autor cadastrado com sucesso!");
                else
                    return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut]
        [Route("{writerId}")]
        public IActionResult Update(int writerId, [FromBody] WriterRequestDTO writer)
        {
            try
            {
                var message = _writerService.Update(writerId, writer);
                if (string.IsNullOrEmpty(message))
                    return Ok("Autor alterado com sucesso!");
                else
                    return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpDelete]
        [Route("{writerId}")]
        public IActionResult Delete(int writerId)
        {
            try
            {
                var message = _writerService.Delete(writerId);
                if (string.IsNullOrEmpty(message))
                    return Ok("Autor deletado com sucesso!");
                else
                    return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}