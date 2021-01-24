using MaiaraBookstore.Data;
using MaiaraBookstore.Models;
using MaiaraBookstore.Models.DTO;
using MaiaraBookstore.Repository.LivroRepository;
using MaiaraBookstore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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

        /// <summary>
        /// Retorna todos os livros cadastrados.
        /// </summary>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="404">Busca sem sucesso, título não encontrado.</response>
        /// <response code="500">Erro ao obter livro.</response>
        [HttpGet("GetLivros")]
        public async Task<ActionResult> GetLivros()
        {
            try 
            {
                LivroRepository livroRepository = new LivroRepository(this._context);

                return Ok(livroRepository.FindAll());
            }
            catch (Exception e) 
            {
                return NotFound("Registro não localizado " + e.Message);
            }
            
        }

        /// <summary>
        /// Retorna todos os livros cadastrados.
        /// </summary>
        /// <param name="livroDTO">Dados do livro que será criado.</param>
        /// <response code="201">Livro salvo com sucesso.</response>
        /// <response code="404">Erro ao salvar livro</response>
        [HttpPost("SalvarLivro")]
        public async Task<ActionResult> SalvarLivro([FromBody] LivroDTO livroDTO)
        {
            LogBookService logBookService = new LogBookService(this._context);
            try
            {
                LivroServiceImpl livroService = new LivroServiceImpl(this._context);

                var resultado = livroService.ValidaSeTituloDeLivroEstaCadastrado(livroDTO.Titulo);
                if (resultado == false)
                {
                    Livro livro = new Livro(livroDTO.Titulo);
                    livroService.SalvarLivro(livro);
                    logBookService.SalvarLog(livro, "Foi registrado o livro: " + livro.Titulo);

                    _context.SaveChanges();
                    return Ok("Livro salvo com sucesso");
                }
                return Ok("Livro já cadastrado");
            }
            catch (Exception e) 
            {
                logBookService.SalvarLog("Erro ao salvar livro");
                return StatusCode(404, "Erro ao salvar Livro " + e.Message);
            }
           
        }

        /// <summary>
        /// Retorna todos os livros cadastrados.
        /// </summary>
        /// <param name="Id">Id do livro a ser deletado.</param>
        /// <response code="200">Título excluído com sucesso</response>
        /// <response code="404">Erro ao deletar.</response>
        [HttpDelete("DeleteLivro/{Id}")]
        public async Task<ActionResult> DeleteLivro([FromHeader] int Id)
        {
            LogBookService logBookService = new LogBookService(this._context);
            try
            {
                LivroServiceImpl livroService = new LivroServiceImpl(this._context);

                var livro = livroService.FindById(Id);
                if (livro != null)
                {
                    logBookService.SalvarLog(livro, "Registro de livro excluído: " + livro.Titulo);
                    livroService.Delete(livro);

                    _context.SaveChanges();
                    return Ok("Livro deletado com sucesso!");
                }
                else
                {
                    return Ok("Não foi possível remover o livro, título não encontrado");
                }
            }
            catch (Exception e) 
            {
                logBookService.SalvarLog("Erro ao excluir Livro");
                return StatusCode(404, "Erro ao excluir Livro " + e.Message);
            }
            
        }

        /// <summary>
        /// Retorna todos os livros cadastrados.
        /// </summary>
        /// <param name="Id">Id do livro editado.</param>
        /// <response code="200">Livro editado com sucesso.</response>
        /// <response code="404">Erro ao Editar.</response>
        [HttpPut("EditLivro/{Id}")]
        public async Task<ActionResult> EditaLivro([FromHeader] int Id, [FromBody] LivroDTO livroDTO)
        {
            LogBookService logBookService = new LogBookService(this._context);
            try
            {
                LivroServiceImpl livroService = new LivroServiceImpl(this._context);

                var livro = livroService.FindById(Id);
                if (livro != null)
                {
                    var livroEditado = livroService.EditaLivro(livro, livroDTO);
                    logBookService.SalvarLog(livro, "Atualização de Livro realizada: " + livro.Titulo);

                    _context.SaveChanges();
                    return Ok(livroEditado);
                }
                else
                {
                    logBookService.SalvarLog("Erro ao editar o livro");
                    return Ok("Não foi possível editar o livro");
                }
            }
            catch (Exception e)
            {
                return StatusCode(404, "Erro ao editar livro" + e.Message);
            }
            
        }
    }
}
