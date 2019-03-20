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
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Produces(typeof(User))]
        [Route("{id}")]
        public IActionResult Get(int id, [FromServices] ContractUserApp contractUserApp)
        {
            try
            {
                return Ok(contractUserApp.GetById(id));
            }
            catch
            {
                return NotFound(id);
            }
        }

        [HttpGet]
        [Produces(typeof(List<User>))]
        //[Route("/user")]
        public List<User> Get([FromQuery]string nome, [FromServices] ContractUserApp contractUserApp)
        {
            try
            {
                return contractUserApp.GetAll(nome);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        //[Route("user")]
        public IActionResult Post([FromBody]UserViewModel body, [FromServices] ContractUserApp contractUserApp)
        {
            try
            {
                contractUserApp.SaveUser(body);
                return Ok(HttpStatusCode.OK);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        //[Route("user")]
        public IActionResult Put([FromBody]UserViewModel body, [FromServices] ContractUserApp contractUserApp)
        {
            try
            {
                contractUserApp.EditUser(body);
                return Ok(HttpStatusCode.OK);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id, [FromServices] ContractUserApp contractUserApp)
        {
            if (id > 0)
                contractUserApp.DeleteUser(id);

            else
                BadRequest();

            return Ok(HttpStatusCode.OK);
        }
    }
}