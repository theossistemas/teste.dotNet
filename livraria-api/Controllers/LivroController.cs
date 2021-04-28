using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using livraria_api.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace livraria_api.Controllers
{
    [Route("livraria-api/[controller]")]
    public class LivroController : ControllerBase
    {

        private ILivroService _livroService;
        private readonly IMapper _mapper;

        public LivroController(ILivroService livroService, IMapper mapper)
        {
            _livroService = livroService;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] LivroModels livroModels)
        {
            if (livroModels == null)
                return NotFound();
            var novoLivro = _mapper.Map<Livro>(livroModels);
            return Execute(() => _livroService.Insert(novoLivro));
        }


        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromBody] LivroModels livroModels)
        {
            if (livroModels == null)
                return NotFound();

            var livro = _livroService.Select(livroModels.Id);
            if (livro == null)
                return NotFound();
            var novoLivro = _mapper.Map<Livro>(livroModels);
            novoLivro.DataCriacao = livro.DataCriacao;
            return Execute(() => _livroService.Update(novoLivro));
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _livroService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            return Execute(() => _livroService.Select());
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _livroService.Select(id));
        }

        [HttpGet("Name")]
        [Authorize]
        public IActionResult GetName(string name)
        {
            if (name == null || name == "")
                return NotFound();

            return Execute(() => _livroService.GetName(name));
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