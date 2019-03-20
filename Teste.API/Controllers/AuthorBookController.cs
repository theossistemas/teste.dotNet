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
    [Route("api/authorBook")]
    [ApiController]
    public class AuthorBookController : ControllerBase
    {
        [HttpGet]
        [Produces(typeof(AuthorBook))]
        [Route("{id}")]
        public IActionResult Get(int id, [FromServices] ContractAuthorBookApp contractAuthorBookApp)
        {
            try
            {
                return Ok(contractAuthorBookApp.GetById(id));
            }
            catch
            {
                return NotFound(id);
            }
        }

        //[HttpGet]
        //[Produces(typeof(List<AuthorBook>))]
        //public List<AuthorBook> Get([FromQuery]string nome, [FromServices] ContractAuthorBookApp contractAuthorBookApp)
        //{
        //    try
        //    {
        //        return contractAuthorBookApp.GetAll();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}


        [HttpGet]
        [Produces(typeof(List<AuthorBook>))]
        public List<AuthorBook> GetAll([FromServices] ContractAuthorBookApp contractAuthorBookApp)
        {
            try
            {
                return contractAuthorBookApp.GetAllAuthorBooks();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]
        public IActionResult Post([FromBody]AuthorBookViewModel body, [FromServices] ContractAuthorBookApp contractAuthorBookApp)
        {
            try
            {
                contractAuthorBookApp.SaveAuthorBook(body);
                return Ok(HttpStatusCode.OK);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]AuthorBookViewModel body, [FromServices] ContractAuthorBookApp contractAuthorBookApp)
        {
            try
            {
                contractAuthorBookApp.EditAuthorBook(body);
                return Ok(HttpStatusCode.OK);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id, [FromServices] ContractAuthorBookApp contractAuthorBookApp)
        {
            if (id > 0)
                contractAuthorBookApp.DeleteAuthorBook(id);

            else
                BadRequest();

            return Ok(HttpStatusCode.OK);
        }
    }
}