using Livraria.API.DTOs.Livros;
using Livraria.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _service;

        public LivrosController(ILivroService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var livros = await _service.ListarLivrosAsync();
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(Guid id)
        {
            var livro = await _service.ObterLivroPorIdAsync(id);
            return Ok(livro); // se não existir, o Service lança exceção → middleware retorna 404
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Criar([FromBody] LivroRequest dto)
        {
            var livro = new Livro(dto.Titulo, dto.Autor, dto.AnoPublicacao, dto.ISBN, dto.NumeroPaginas,
                                  dto.Editora, dto.CategoriaId, dto.Idioma, dto.Preco, dto.Sinopse);

            var resultado = await _service.CriarLivroAsync(livro);
            return CreatedAtAction(nameof(Obter), new { id = resultado.Id }, resultado);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] LivroUpdateRequest dto)
        {
            var livro = new Livro(dto.Titulo, dto.Autor, dto.AnoPublicacao, dto.Preco);
            typeof(Livro).GetProperty("Id").SetValue(livro, id);

            var resultado = await _service.AtualizarLivroAsync(livro);
            return Ok(resultado);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var removido = await _service.DeletarLivroAsync(id);
            return NoContent(); // se não existir, Service lança exceção → middleware retorna 404
        }
    }
}
