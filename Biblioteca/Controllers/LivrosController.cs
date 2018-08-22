using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LivrosController : Controller
    {
        private ILivroService _livroService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        public LivrosController(
            ILivroService livroService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _livroService = livroService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        [HttpGet]
        public List<Livro> GetAll()
        {
            return _livroService.GetAll().OrderBy(i=>i.Nome).ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public Livro GetById(long id)
        {
            var item = _livroService.GetById(id);
            if (item == null)
            {
                return new Livro();
            }
            return item;
        }   
        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody]Livro item)
        {            
            if(string.IsNullOrEmpty(item.Nome))
                return null;
            _livroService.Create(item);

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(long id,[FromBody]Livro item)
        {
            var todo = _livroService.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Nome = item.Nome;
            todo.Autor = item.Autor;

            _livroService.Update(todo);
            return NoContent();
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var livro = _livroService.GetById(id);
            if (livro == null)
            {
                return NotFound();
            }

            _livroService.Delete(livro.Id);
            return NoContent();
        }
    }
}