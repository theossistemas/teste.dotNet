using Gerenciador.Livraria.Core.Business.Livraria;
using Gerenciador.Livraria.Core.Interfaces.BusinessInterface;
using Gerenciador.Livraria.DTOs.DTOs.Livros;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador.Livraria.API.Controllers.Categorias
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaBusiness _categoriaBusiness;

        public CategoriasController(ICategoriaBusiness categoriaBusiness)
        {
            _categoriaBusiness = categoriaBusiness;
        }

        [Authorize]
        [HttpPost("cadastrarCategoria")]
        public async Task<IActionResult> CadastrarCategoria([FromBody] CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO is null)
                return BadRequest("Nenhum dado inserido para cadastrar uma nova categoria");

            var cadastroDaCategoria = await _categoriaBusiness.CadastrarNovaCategoria(categoriaDTO);
            return CreatedAtAction(null, categoriaDTO);
        }

        [AllowAnonymous]
        [HttpGet("listarCategoriasCadastradas")]
        public async Task<IActionResult> ListarCategoriasCadastradas()
        {
            var categoriasCadastradas = await _categoriaBusiness.ListarCategorias();

            if (categoriasCadastradas is null)
                return NotFound();

            return Ok(categoriasCadastradas);
        }

        [AllowAnonymous]
        [HttpGet("pesquisarCategoriaPeloId/{id}")]
        public IActionResult PesquisarCategoriaPeloId(int id)
        {
            var categoriaNome = _categoriaBusiness.PesquisarCategoriaPeloId(id);

            if (categoriaNome is null)
                return NotFound();

            return Ok(categoriaNome);
        }

        [Authorize]
        [HttpPut("atualizarCategoriaCadastrada")]
        public async Task<IActionResult> AtualizarCategoriaCadastrada([FromBody] CategoriaDTO categoriaDTO)
        {
            var categoriaAtualizada = await _categoriaBusiness.AtualizarCategoria(categoriaDTO);

            if (categoriaAtualizada is null)
                return NotFound();

            return Ok(categoriaAtualizada);
        }

        [Authorize]
        [HttpDelete("excluirCategoria/{id}")]
        public async Task<IActionResult> ExcluirRegistroFisicoDaCategoria(int id)
        {
            var registrdoExcluido = await _categoriaBusiness.ExcluirCategoria(id);
            if (!registrdoExcluido)
                return NotFound("Categoria não encontrada.");

            return NoContent();
        }
    }
}
