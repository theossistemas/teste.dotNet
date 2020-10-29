using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using LivrariaTheos.WebApp.Data;
using Repository.Contracts;
using Service.Contracts;

namespace LivrariaTheos.WebApp.ControllersApi
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LivrosController : ControllerBase
    {
        #region Properties
        private readonly IService<Livros> _service;
        #endregion

        #region Constructor
        public LivrosController(IService<Livros> service)
        {
            _service = service;
        }
        #endregion

        #region Methods
        // GET: api/Livros
        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(Livros))]
        [ProducesResponseType(statusCode: 404)]
        public async Task<ActionResult<IEnumerable<Livros>>> GetLivros()
        {
            return Ok(await _service.FindAllAsync());
        }

        // GET: api/Livros/5
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Livros))]
        [ProducesResponseType(statusCode: 404)]
        public async Task<ActionResult<Livros>> GetLivros(int id)
        {
            try
            {
                return Ok(await _service.FindAsync(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/Livros/5
        [HttpPut("{id}")]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> PutLivros(Livros model)
        {
            try
            {
                await _service.UpdateChangesAsync(model);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST: api/Livros
        [HttpPost]
        [ProducesResponseType(statusCode: 404)]
        public async Task<ActionResult<Livros>> PostLivros(Livros model)
        {
            if (!BuscarLivroRegistrado(model))
            {
                await _service.InsertAsync(model);
                return Ok();
            }

            return BadRequest(new Exception("Livro já cadastrado"));
        }

        // DELETE: api/Livros/5
        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode: 404)]
        public async Task<ActionResult<Livros>> DeleteLivros(int id)
        {
            try
            {
                await _service.RemoveAsync(id);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        #endregion

        #region Private methods
        private bool BuscarLivroRegistrado(Livros model)
        {
            var list = _service.FindAllAsync().Result;
            foreach (var item in list)
            {
                if (item.NomeLivro == model.NomeLivro)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
