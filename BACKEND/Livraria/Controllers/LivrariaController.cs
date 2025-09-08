using Livraria.Domain.Exceptions;
using Livraria.DTOs;
using Livraria.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LivrariaController : ControllerBase
    {
        #region[Construtor]
        private readonly ILivroCreateService _livroCreateService;
        private readonly ILivroReadService _livroReadService;
        private readonly ILivroUpdateService _livroUpdateService;
        private readonly ILivroDeleteService _livroDeleteService;

        public LivrariaController(ILivroCreateService livroCreateService, ILivroReadService livroReadService, ILivroUpdateService livroUpdateService, ILivroDeleteService livroDeleteService)
        {
            _livroCreateService = livroCreateService;
            _livroReadService = livroReadService;
            _livroUpdateService = livroUpdateService;
            _livroDeleteService = livroDeleteService;
        }


        #endregion

        #region[Get]

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<LivroDTO>>> GetLivros()
        {
            var livros = await _livroReadService.GetLivrosAsync();
            return Ok(livros);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<LivroDTO>> GetLivro(int id)
        {
            try
            {
                var livro = await _livroReadService.GetLivroByIdAsync(id);
                if (livro == null)
                    throw new NaoEncontradoException($"O livro com o id {id} não foi encontrado.");

                return Ok(livro);
            }
            catch (ConflitoDeDadosException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (OperacaoNaoPermitidaException ex)
            {
                return Forbid();
            }
            catch (NaoEncontradoException ex)
            {
                return NotFound();
            }
        }

        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLivrosPaged(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var (livros, totalCount) = await _livroReadService.GetLivrosPagedAsync(pageNumber, pageSize);
                return Ok(new { livros, totalCount });
            }
            catch (ConflitoDeDadosException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (OperacaoNaoPermitidaException ex)
            {
                return Forbid();
            }
            catch (NaoEncontradoException ex)
            {
                return NotFound();
            }
        }

        #endregion

        #region[Post]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LivroDTO>> PostLivro(LivroDTO livro)
        {
            try
            {
                var livroCriado = await _livroCreateService.AddLivroAsync(livro);
                return CreatedAtAction(nameof(GetLivro), new { id = livroCriado.Id }, livroCriado);
            }
            catch (ConflitoDeDadosException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (OperacaoNaoPermitidaException ex)
            {
                return Forbid();
            }
            catch (NaoEncontradoException ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region[Put]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, LivroDTO livro)
        {
            try
            {
                if (id != livro.Id)
                    return BadRequest();

                await _livroUpdateService.UpdateLivroAsync(id, livro);
                return NoContent();
            }
            catch (ConflitoDeDadosException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (NaoEncontradoException ex)
            {
                return NotFound();
            }
            catch (OperacaoNaoPermitidaException ex)
            {
                return Forbid();
            }
        }
        #endregion

        #region[Delete]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            try
            {
                var resultado = await _livroDeleteService.DeleteLivroAsync(id);
                if (!resultado)
                    throw new NaoEncontradoException($"O livro com o id {id} não foi encontrado ou já foi excluído.");

                return NoContent();
            }
            catch (ConflitoDeDadosException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (OperacaoNaoPermitidaException)
            {
                return Forbid();
            }
            catch (NaoEncontradoException)
            {
                return NotFound(); // <-- sem argumentos!
            }
            #endregion
        }
    }
}
