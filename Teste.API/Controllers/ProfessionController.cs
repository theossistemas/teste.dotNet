using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teste.App.Services;
using Teste.App.viewModel;

namespace Teste.API.Controllers
{
    [Route("api/profession")]
    [ApiController]
    public class ProfessionController : ControllerBase
    {
        [HttpGet]
        [Produces(typeof(Profession))]
        [Route("{id}")]
        public IActionResult Get(int id, [FromServices] ContractProfessionApp contractProfessionApp)
        {
            try
            {
                return Ok(contractProfessionApp.GetById(id));
            }
            catch
            {
                return NotFound(id);
            }
        }

        [HttpGet]
        [Produces(typeof(List<Profession>))]
        //[Route("/profession")]
        public List<Profession> Get([FromQuery]string nome, [FromServices] ContractProfessionApp contractProfessionApp)
        {
            try
            {
                return contractProfessionApp.GetAll(nome);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        //[Route("profession")]
        public IActionResult Post([FromBody]ProfessionViewModel body, [FromServices] ContractProfessionApp contractProfessionApp)
        {
            try
            {
                contractProfessionApp.SaveProfession(body);
                return Ok(HttpStatusCode.OK);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        //[Route("profession")]
        public IActionResult Put([FromBody]ProfessionViewModel body, [FromServices] ContractProfessionApp contractProfessionApp)
        {
            try
            {
                contractProfessionApp.EditProfession(body);
                return Ok(HttpStatusCode.OK);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id, [FromServices] ContractProfessionApp contractProfessionApp)
        {
            if (id > 0)
                contractProfessionApp.DeleteProfession(id);

            else
                BadRequest();

            return Ok(HttpStatusCode.OK);
        }
    }
}