using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teste.App.Services;
using Teste.App.viewModel;

namespace Teste.API.Controllers
{
    [Route("api/state")]
    [ApiController]
    public class StateController : ControllerBase
    {
        [HttpGet]
        [Produces(typeof(State))]
        [Route("{id}")]
        public IActionResult Get(int id, [FromServices] ContractStateApp contractStateApp)
        {
            try
            {
                return Ok(contractStateApp.GetById(id));
            }
            catch
            {
                return NotFound(id);
            }
        }

        [HttpGet]
        [Produces(typeof(List<StateViewModel>))]
        public List<StateViewModel> Get([FromQuery]string name, [FromServices] ContractStateApp contractStateApp)
        {
            try
            {
                return contractStateApp.GetAll(name);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}