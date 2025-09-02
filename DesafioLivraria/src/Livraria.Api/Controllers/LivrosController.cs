using Livraria.Application.Dtos;
using Livraria.Application.Interfaces;
using Livraria.Application.Validations;
using Livraria.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly LivroValidation _livroValidation;

        private readonly ILivro _livroService;

        public LivrosController(ILivro livroService, LivroValidation livroValidation)
        {
            _livroService = livroService;
            _livroValidation = livroValidation;
        }

        [HttpGet]
        public async Task<IEnumerable<Livro>> GetAll()
        {
            return await _livroService.GetAllAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LivroCreateDto livroDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ocorrencias = await _livroValidation.ValidarIsbnUnico(livroDto);

            if (ocorrencias != null)
                return Conflict(new { message = ocorrencias });

            var livro = new Livro
            {
                Titulo = livroDto.Titulo,
                Autor = livroDto.Autor,
                AnoPublicacao = livroDto.AnoPublicacao,
                Isbn = livroDto.Isbn
            };

            await _livroService.AddAsync(livro);
            return CreatedAtAction(nameof(GetAll), new { id = livro.Id }, livro);
        }
    }
}
