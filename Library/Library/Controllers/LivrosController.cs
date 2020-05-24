using AutoMapper;
using Business.Services.Interface;
using Library.Dto;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entity;
using System;
using System.Collections.Generic;

namespace Livraria.Controllers
{
    [Route("api/Livro")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivrosServices _livrosServices;
        private readonly IMapper _mapper;

        public LivrosController(ILivrosServices livrosServices, IMapper mapper)
        {
            _livrosServices = livrosServices;
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                var livro = _livrosServices.GetAll();
                return Ok(_mapper.Map<IEnumerable<LivroViewModel>>(livro));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                var livro = _livrosServices.GetById(id);
                return Ok(_mapper.Map<LivroViewModel>(livro));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] LivroDto livroDto)
        {
            try
            {
                var livro = _mapper.Map<Livro>(livroDto);
                _livrosServices.AddLivro(livro);
                return Ok(_livrosServices.Save());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LivroDto livroDto)
        {
            try
            {
                var livro = _mapper.Map<Livro>(livroDto);
                return Ok(_livrosServices.EditLivro(id, livro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message) ;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _livrosServices.RemoveLivro(id);
                return Ok(_livrosServices.Save());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
