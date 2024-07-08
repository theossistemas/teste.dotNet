using Gerenciador.Livraria.Core.Interfaces.BusinessInterface;
using Gerenciador.Livraria.DTOs.DTOs.Livros;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador.Livraria.API.Controllers.Autores
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorBusiness _autorBusiness;

        public AutoresController(IAutorBusiness autorBusiness)
        {
            _autorBusiness = autorBusiness;
        }

        [Authorize]
        [HttpPost("cadastrarAutor")]
        public async Task<IActionResult> CadastrarAutor([FromBody] AutorDTO autorDTO)
        {
            if (autorDTO is null)
                return BadRequest("Nenhum dado inserido para cadastrar um novo Autor");

            var cadastroDoAutor = await _autorBusiness.CadastrarNovoAutor(autorDTO);
            return CreatedAtAction(null, autorDTO);
        }

        [AllowAnonymous]
        [HttpGet("pesquisarAutorPeloId/{id}")]
        public IActionResult PesquisarAutorPeloId(int id)
        {
            var autorNome = _autorBusiness.PesquisarAutorPeloId(id);

            if (autorNome is null)
                return NotFound();

            return Ok(autorNome);
        }

        [Authorize]
        [HttpPut("atualizarDadosDoAutor")]
        public async Task<IActionResult> AtualizarDadosDoAutor([FromBody] AutorDTO autorDTO)
        {
            if (autorDTO == null)
            {
                return BadRequest("Nenhum dado inserido para ser atualizado.");
            }

            var dadosDoAutorAtualizados = await _autorBusiness.AtualizarDadosDoAutor(autorDTO);
            return Ok(dadosDoAutorAtualizados);
        }

        [Authorize]
        [HttpDelete("excluirAutor/{id}")]
        public async Task<IActionResult> ExcluirRegistroFisicoDoAutor(int id)
        {
            var registrdoExcluido = await _autorBusiness.ExcluirRegistroFisicoDoAutor(id);
            if (!registrdoExcluido)
                return NotFound("Autor não encontrado.");

            return NoContent();
        }
    }
}
