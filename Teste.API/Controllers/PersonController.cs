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
    [Route("api/person")]
    [ApiController]
    public class PersonController : Controller
    {
        [HttpGet]
        [Produces(typeof(Person))]
        [Route("{id}")]
        public IActionResult Get(int id, [FromServices] ContractPersonApp contractPersonApp)
        {
            try
            {
                return Ok(contractPersonApp.GetById(id));
            }
            catch
            {
                return NotFound(id);
            }
        }

        [HttpGet]
        [Produces(typeof(List<Person>))]
        //[Route("/person")]
        public List<Person> Get([FromQuery]string name, [FromServices] ContractPersonApp contractPersonApp)
        {
            try
            {
                return contractPersonApp.GetAll(name);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        //[Route("person")]
        public IActionResult Post([FromBody]PersonViewModel body, [FromServices] ContractPersonApp contractPersonApp)
        {
            try
            {
                return new ObjectResult(contractPersonApp.SavePerson(body));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        //[Route("person")]
        public IActionResult Put([FromBody]PersonViewModel body, [FromServices] ContractPersonApp contractPersonApp)
        {
            try
            {
                contractPersonApp.EditPerson(body);
                return Ok(HttpStatusCode.OK);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id, [FromServices] ContractPersonApp contractPersonApp)
        {
            if (id > 0)
                contractPersonApp.DeletePerson(id);

            else
                BadRequest();

            return Ok(HttpStatusCode.OK);
        }
    }
}
