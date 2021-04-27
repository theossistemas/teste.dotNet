using Livraria.Service.Validators;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using livraria_api.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Livraria.Domain.Security.Interfaces;
using Livraria.Domain.Security.Models;
using livraria_api.Services;

namespace livraria_api.Controllers
{
    [Route("livraria-api/[controller]")]
    public class LivroController : ControllerBase
    {

        private IBaseService<Livro> _baseLivroService;        

        public LivroController(IBaseService<Livro> baseLivroService)
        {
            _baseLivroService = baseLivroService;            
        }       

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] LivroModels livroModels)
        {
            if (livroModels == null)
                return NotFound();

            return  Execute(() => _baseLivroService.Add<LivroModels, LivroModels, LivroValidator>(livroModels));            
        }
        

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromBody] LivroModels livroModels)
        {
            if (livroModels == null)
                return NotFound();

            var existe = _baseLivroService.VerifyLivro(livroModels.Id);
            if (!existe)
                return NotFound();

            return Execute(() => _baseLivroService.Update<LivroModels, LivroModels, LivroValidator>(livroModels));            
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseLivroService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            return Execute(() => _baseLivroService.Get<LivroModels>());
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseLivroService.GetById<LivroModels>(id));
        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}