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
    [Route("api/trainingArea")]
    [ApiController]
    public class TrainingAreaController : ControllerBase
    {
        [HttpGet]
        [Produces(typeof(TrainingArea))]
        [Route("{id}")]
        public IActionResult Get(int id, [FromServices] ContractTrainingAreaApp contractTrainingAreaApp)
        {
            try
            {
                return Ok(contractTrainingAreaApp.GetById(id));
            }
            catch
            {
                return NotFound(id);
            }
        }

        [HttpGet]
        [Produces(typeof(List<TrainingArea>))]
        //[Route("/trainingArea")]
        public List<TrainingArea> Get([FromQuery]string nome, [FromServices] ContractTrainingAreaApp contractTrainingAreaApp)
        {
            try
            {
                return contractTrainingAreaApp.GetAll(nome);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        //[Route("trainingArea")]
        public IActionResult Post([FromBody]TrainingAreaViewModel body, [FromServices] ContractTrainingAreaApp contractTrainingAreaApp)
        {
            try
            {
                contractTrainingAreaApp.SaveTrainingArea(body);
                return Ok(HttpStatusCode.OK);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        //[Route("trainingArea")]
        public IActionResult Put([FromBody]TrainingAreaViewModel body, [FromServices] ContractTrainingAreaApp contractTrainingAreaApp)
        {
            try
            {
                contractTrainingAreaApp.EditTrainingArea(body);
                return Ok(HttpStatusCode.OK);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id, [FromServices] ContractTrainingAreaApp contractTrainingAreaApp)
        {
            if (id > 0)
                contractTrainingAreaApp.DeleteTrainingArea(id);

            else
                BadRequest();

            return Ok(HttpStatusCode.OK);
        }
    }
}