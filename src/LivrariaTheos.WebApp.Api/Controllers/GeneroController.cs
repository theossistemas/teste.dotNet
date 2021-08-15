using AutoMapper;
using LivrariaTheos.Estoque.Application.Services;
using LivrariaTheos.Estoque.Domain.Dtos;
using LivrariaTheos.Estoque.Domain.Generos;
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
        ArmazenadorDeGenero _armazenadorDeGenero;
        private readonly IMapper _mapper;

        public GeneroController(IGeneroAppService generoAppService,
            ArmazenadorDeGenero armazenadorDeGenero,
            IMapper mapper,
            ArmazenadorDeLogAplicacao armazenadorDeLogAplicacao) : base(armazenadorDeLogAplicacao)
        {
            _generoAppService = generoAppService;
            _armazenadorDeGenero = armazenadorDeGenero;
            _mapper = mapper;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarGenero(int id, GeneroDto generoDto)
        {
            if (id != generoDto.Id)
            {
                return BadRequest();
            }

            var Livro = await _generoAppService.ObterPorId(id);

            if (Livro == null)
            {
                return NotFound();
            }

            try
            {
                var resultado = await _armazenadorDeGenero.Armazenar(_mapper.Map<Genero>(generoDto));

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GeneroDto>> CriarGenero(GeneroDto generoDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var resultado = await _armazenadorDeGenero.Armazenar(_mapper.Map<Genero>(generoDto));

                return Ok(_mapper.Map<GeneroDto>(resultado));
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirGenero(int id)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _armazenadorDeGenero.Excluir(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }
    }
}
