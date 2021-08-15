using LivrariaTheos.Estoque.Domain.Dtos;
using LivrariaTheos.Estoque.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivrariaTheos.Estoque.Domain.Logs;

namespace LivrariaTheos.WebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : BaseController
    {
        private readonly IAutorAppService _autorAppService;

        public AutorController(IAutorAppService autorAppService, 
            ArmazenadorDeLogAplicacao armazenadorDeLogAplicacao): base (armazenadorDeLogAplicacao)
        {
            _autorAppService = autorAppService;
        }

        [HttpGet("ObterAutor/{id}")]
        public async Task<ActionResult<AutorDto>> ObterAutor(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _autorAppService.ObterPorId(id);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterPorNome/{nome}")]
        public async Task<ActionResult<IEnumerable<AutorDto>>> ObterPorNome(string nome)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _autorAppService.ObterPorNome(nome);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterAutoresAtivos")]
        public async Task<ActionResult<IEnumerable<AutorDto>>> ObterAutoresAtivos()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _autorAppService.ObterTodosAtivos();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterAutores")]
        public async Task<ActionResult<IEnumerable<AutorDto>>> ObterAutores()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _autorAppService.ObterTodos();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterPorNacionalidade/{nacionalidadeId}")]
        public async Task<ActionResult<IEnumerable<AutorDto>>> ObterPorNacionalidade(int nacionalidadeId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _autorAppService.ObterPorNacionalidade(nacionalidadeId);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarAutor(int id, AutorDto autorDto)
        {
            if (id != autorDto.Id)
            {
                return BadRequest();
            }

            var autor = await _autorAppService.ObterPorId(id);

            if (autor == null)
            {
                return NotFound();
            }

            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }           
        }        
    }
}
