using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teste.App.Services;

namespace Teste.API.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {

        [HttpGet]
        //[Produces(typeof(List<City>))]
        public IActionResult Get([FromQuery]int stateId, [FromServices] ContractCityApp contractCityApp)
        {
            try
            {
                return new ObjectResult(contractCityApp.GetAllCitiesByStateId(stateId));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}