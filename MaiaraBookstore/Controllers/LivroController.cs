using MaiaraBookstore.Data;
using MaiaraBookstore.Models;
using MaiaraBookstore.Models.DTO;
using MaiaraBookstore.Repository.LivroRepository;
using MaiaraBookstore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MaiaraBookstore.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly DataContext _context;

        public LivroController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet("GetLivros")]
        public async Task<ActionResult> GetLivros()
        {
            LivroRepository livroRepository = new LivroRepository(this._context);
            return Ok(livroRepository.FindAll());
        }

        [HttpPost("SalvarLivro")]
        public async Task<ActionResult> SalvarLivro([FromBody] LivroDTO livroDTO)
        {
            LivroServiceImpl livroService = new LivroServiceImpl(this._context);
            var resultado = livroService.ValidaSeTituloDeLivroEstaCadastrado(livroDTO.Titulo);
            if (resultado == false)
            {
                livroService.SalvarLivro(new Livro(livroDTO.Titulo));
                return Ok("Livro salvo com sucesso");
            }

            return Ok("Livro já cadastrado");
        }

        [HttpDelete("DeleteLivro/{Id}")]
        public async Task<ActionResult> DeleteLivro([FromHeader] int Id)
        {
            LivroServiceImpl livroService = new LivroServiceImpl(this._context);
            var livro = livroService.FindById(Id);
            if (livro != null)
            {
                livroService.Delete(livro);
                return Ok("Livro deletado com sucesso!");
            }
            else
            {
                return Ok("Não foi possível remover o livro, título não encontrado");
            }
        }

        [HttpPut("EditLivro/{Id}")]
        public async Task<ActionResult> EditaLivro([FromHeader] int Id, [FromBody] LivroDTO livroDTO)
        {
            LivroServiceImpl livroService = new LivroServiceImpl(this._context);
            var livro = livroService.FindById(livroDTO.Id);
            if (livro != null)
            {
                var livroEditado = livroService.EditaLivro(livro, livroDTO);
                return Ok(livroEditado);
            }
            else 
            {
                return Ok("Não foi possível editar o livro, título não encontrado");
            }
        }
    }
}
