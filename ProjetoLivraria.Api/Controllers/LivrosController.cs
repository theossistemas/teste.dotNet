using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjetoLivraria.Application.Interfaces;
using ProjetoLivraria.Application.ViewModels;
using System.Linq;

namespace ProjetoLivraria.Api.Controllers
{
    [Route("api/[controller]")]
    public class LivrosController : Controller
    {
        private readonly ILivroAppService _service;

        public LivrosController(ILivroAppService service)
        {
            _service = service;
            if(!_service.GetAll().Any())
            {
                var livro = new LivroViewModel
                {
                    Autor = "Chas Emerick, Brian Carper & Christophe Grand",
                    ImagemCapa = "https://images.gr-assets.com/books/1344675203l/10883803.jpg",
                    Isbn = "9781449394707",
                    Preco = 170.77,
                    Publicacao = new DateTime(2012, 04, 21),
                    Titulo = "Clojure Programming"
                };

                _service.Register(livro);
            }
        }

        [HttpGet]
        public IEnumerable<LivroViewModel> Get()
        {
            return _service.GetAllOrderByTitle();
        }

        [HttpGet("{id}")]
        public ActionResult<LivroViewModel> Get(Guid id)
        {
            var livro = _service.GetById(id);

            if (livro == null)
                return NotFound();
            return livro;
        }

        [HttpPost]
        public ActionResult<LivroViewModel> Post(LivroViewModel livroViewModel)
        {
            try
            {
                _service.Register(livroViewModel);
                return CreatedAtAction(nameof(Get), new { id = livroViewModel.Id }, livroViewModel);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<LivroViewModel> Put(LivroViewModel livro)
        {
            try
            { 
                if (_service.GetById(livro.Id) != null)
                {
                    _service.Update(livro);
                    return NoContent();
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<LivroViewModel> Delete(Guid id)
        {
            var livro = _service.GetById(id);

            if (livro == null)
                return NotFound();

            _service.Remove(id);
            return NoContent();
        }
    }
}
