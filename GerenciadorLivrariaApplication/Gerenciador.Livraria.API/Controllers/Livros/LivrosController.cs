using Gerenciador.Livraria.Core.Interfaces.BusinessInterface;
using Gerenciador.Livraria.DTOs.DTOs.Livros;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador.Livraria.API.Controllers.Livros
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivrariaBusiness _livrariaBusiness;

        public LivrosController(ILivrariaBusiness livrariaBusiness)
        {
            _livrariaBusiness = livrariaBusiness;
        }

        [AllowAnonymous]
        [HttpGet("listarObrasCadastradas")]
        public async Task<IActionResult> ListarObrasCadastradas()
        {
            var obrasCadastradas = await _livrariaBusiness.ListarObras();

            if (obrasCadastradas is null)
                return NotFound();

            return Ok(obrasCadastradas);
        }

        [Authorize]
        [HttpPost("cadastrarNovaObra")]
        public async Task<IActionResult> CadastrarNovaObra([FromBody] LivroDTO livroDTO)
        {
            if (livroDTO == null)
                return BadRequest("Nenhuma informação foi inserida para ser cadastrada.");

            try
            {
                await _livrariaBusiness.CadastrarObra(livroDTO);
                return CreatedAtAction(null, livroDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("excluirObra/{id}")]
        public async Task<IActionResult> ExcluirObra(int id)
        {
            var registroExcluido = await _livrariaBusiness.ExcluirObra(id);
            
            if (!registroExcluido) 
                return NotFound("Obra não encontrada.");

            return NoContent();
        }

        [Authorize]
        [HttpPut("atualizarObra")]
        public async Task<IActionResult> AtualizarObra([FromBody] LivroDTO livroDTO)
        {
            if (livroDTO == null)
                return BadRequest("Nenhum dado inserido para ser atualizado.");

            await _livrariaBusiness.AtualizarObra(livroDTO);
            return Ok();
        }
    }
}
