using Livraria.Common.Model;
using Livraria.Common.Utils;
using Livraria.Domain.Dto;
using Livraria.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : BaseController
    {
        private readonly IAutorRepositorio _autorRepositorio;
        public AutorController(
            INotificationHandler<Notifications> notification,
            IAutorRepositorio autorRepositorio) : base(notification)
        {
            _autorRepositorio = autorRepositorio;
        }

        [HttpGet]
        [Authorize(Roles ="manager")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var resultados = await _autorRepositorio.ListarAsync();
                return Ok(resultados.Select(x => AutorDto.ConverterParaDto(x)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resources.MSG_Status500);
            }
        }

    }
}
