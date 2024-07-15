using LivrariaWeb.Models;
using LivrariaWeb.Services.Livro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;
        private readonly ILogger<LivroService> _logger;

        public LivroController(ILivroService livroInterface, ILogger<LivroService> logger)
        {
            _livroService = livroInterface;
            _logger = logger;
        }

        [HttpGet("ListarLivros")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<LivroModel>>> ListarLivros()
        {
            var livros = await _livroService.ListarLivros();
            return Ok(livros);
        }

        [HttpGet("BuscarLivroPorId/{idLivro}")]
        [AllowAnonymous]
        public async Task<ActionResult<LivroModel>> BuscarLivroPorId(int? idLivro)
        {
            var livro = await _livroService.BuscarLivroPorId(idLivro);
            return Ok(livro);
        }

        [HttpPost("CriarLivro")]
        public async Task<ActionResult<List<LivroModel>>> CriarLivro(LivroModel livro)
        {
            var livros = await _livroService.CriarLivro(livro);
            return Ok(livros);
        }


        [HttpPut("EditarLivro")]
        public async Task<ActionResult<List<LivroModel>>> EditarLivro(LivroModel livro)
        {
            var livros = await _livroService.EditarLivro(livro);
            return Ok(livros);
        }

        [HttpDelete("ExcluirLivro")]
        public async Task<ActionResult<List<LivroModel>>> ExcluirLivro(int? idLivro)
        {
            var livros = await _livroService.ExcluirLivro(idLivro);
            return Ok(livros);
        }
    }
}
