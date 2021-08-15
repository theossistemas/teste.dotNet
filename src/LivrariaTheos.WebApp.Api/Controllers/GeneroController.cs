using LivrariaTheos.Estoque.Application.Services;
using LivrariaTheos.Estoque.Domain.Dtos;
using LivrariaTheos.Estoque.Domain.Logs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaTheos.WebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : BaseController
    {
        private readonly IGeneroAppService _generoAppService;

        public GeneroController(IGeneroAppService generoAppService,
            ArmazenadorDeLogAplicacao armazenadorDeLogAplicacao) : base(armazenadorDeLogAplicacao)
        {
            _generoAppService = generoAppService;
        }

        [HttpGet("ObterGenero/{id}")]
        public async Task<ActionResult<GeneroDto>> ObterGenero(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _generoAppService.ObterPorId(id);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterPorNome/{nome}")]
        public async Task<ActionResult<IEnumerable<GeneroDto>>> ObterPorNome(string nome)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _generoAppService.ObterPorNome(nome);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterGenerosAtivos")]
        public async Task<ActionResult<IEnumerable<GeneroDto>>> ObterGenerosAtivos()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _generoAppService.ObterTodosAtivos();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterGeneros")]
        public async Task<ActionResult<IEnumerable<GeneroDto>>> ObterGeneros()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _generoAppService.ObterTodos();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }
    }
}
