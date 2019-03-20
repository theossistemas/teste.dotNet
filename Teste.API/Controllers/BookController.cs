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
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet]
        [Produces(typeof(Book))]
        [Route("{id}")]
        public IActionResult Get(int id, [FromServices] ContractBookApp contractBookApp)
        {
            try
            {
                return Ok(contractBookApp.GetById(id));
            }
            catch
            {
                return NotFound(id);
            }
        }

        [HttpGet]
        [Produces(typeof(List<Book>))]
        //[Route("/book")]
        public List<Book> Get([FromQuery]string nome, [FromServices]  ContractBookApp contractBookApp)
        {
            try
            {
                return contractBookApp.GetAll(nome);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        //[Route("book")]
        public IActionResult Post([FromBody]BookViewModel body, [FromServices] ContractBookApp contractBookApp)
        {
            try
            {
                //return Ok(HttpStatusCode.OK);
                return new ObjectResult(contractBookApp.SaveBook(body));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        //[Route("book")]
        public IActionResult Put([FromBody]BookViewModel body, [FromServices] ContractBookApp contractBookApp)
        {
            try
            {
                contractBookApp.EditBook(body);
                return Ok(HttpStatusCode.OK);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id, [FromServices] ContractBookApp contractBookApp)
        {
            if (id > 0)
                contractBookApp.DeleteBook(id);

            else
                BadRequest();

            return Ok(HttpStatusCode.OK);
        }
    }
}