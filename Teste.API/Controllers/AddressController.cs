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
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        [HttpGet]
        [Produces(typeof(Address))]
        [Route("{id}")]
        public IActionResult Get(int id, [FromServices] ContractAddressApp contractAddressApp)
        {
            try
            {
                return Ok(contractAddressApp.GetById(id));
            }
            catch
            {
                return NotFound(id);
            }
        }

        [HttpGet]
        [Produces(typeof(List<Address>))]
        public List<Address> Get([FromQuery]string nome, [FromServices] ContractAddressApp contractAddressApp)
        {
            try
            {
                return contractAddressApp.GetAll(nome);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody]AddressViewModel body, [FromServices] ContractAddressApp contractAddressApp)
        {
            try
            {
                return new ObjectResult(contractAddressApp.SaveAddress(body));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]AddressViewModel body, [FromServices] ContractAddressApp contractAddressApp)
        {
            try
            {
                contractAddressApp.EditAddress(body);
                return Ok(HttpStatusCode.OK);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id, [FromServices] ContractAddressApp contractAddressApp)
        {
            if (id > 0)
                contractAddressApp.DeleteAddress(id);

            else
                BadRequest();

            return Ok(HttpStatusCode.OK);
        }

        [HttpGet]
        [Produces(typeof(Address))]
        [Route("person")]
        public IActionResult GetAddressByPersonId([FromQuery]int personId, [FromServices] ContractAddressApp contractAddressApp)
        {
            try
            {
                return new ObjectResult(contractAddressApp.GetAddressByPersonId(personId));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}