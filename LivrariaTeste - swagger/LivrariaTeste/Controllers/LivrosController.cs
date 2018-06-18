using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivrariaTeste.Data;
using LivrariaTeste.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaTeste.Controllers
{
   [Produces("application/json")]
   [Route("api/Livros")]
   public class LivrosController : Controller
   {
      private readonly LivrariaTesteContext _context;

      public LivrosController(LivrariaTesteContext context)
      {
         _context = context;
      }

      // GET: api/Livros
      [HttpGet]
      public IActionResult GetLivros()
      {
         var livros = from s in _context.Livros select s;

         livros = livros.OrderBy(c => c.Descricao);

         return Ok(livros);
      }

      // GET: api/Livros/5
      [HttpGet("{id}", Name = "GetLivrosById")]
      public IActionResult GetLivrosById(int id)
      {
         var livros = from s in _context.Livros select s;

         livros = livros.Where(l => l.id == id);

         if (livros == null)
            return NotFound();

         return Ok(livros.FirstOrDefault());
      }

      // POST: api/Livros
      [HttpPost]
      public IActionResult InserirLivro([FromBody]Livros livros)
      {
         try
         {
            _context.Add(livros);
            _context.SaveChanges();

            return CreatedAtRoute("GetLivrosById", new { id = livros.id }, livros);
         }
         catch (Exception ex)
         {
            return Json(new { status = "error", message = "Erro ao inserir livro. Motivo:\n" + ex.Message });
         }
      }

      // PUT: api/Livros/5
      [HttpPut("{id}")]
      public IActionResult AlterarLivro(int id, [FromBody]Livros livros)
      {
         try
         {
            Livros mdLivros = new Livros();

            mdLivros = _context.Livros.Where(l => l.id == id).FirstOrDefault();

            if (livros == null)
               return BadRequest();

            if (mdLivros == null)
               return NotFound();

            mdLivros.Descricao = livros.Descricao;
            mdLivros.valor = livros.valor;

            _context.SaveChanges();
            return CreatedAtRoute("GetLivrosById", new { id = mdLivros.id }, mdLivros);
         }
         catch (Exception ex)
         {
            return Json(new { status = "error", message = "Erro ao alterar livro. Motivo:\n" + ex.Message });
         }
      }

      // DELETE: api/ApiWithActions/5
      [HttpDelete("{id}")]
      public IActionResult ExcluirLivro(int id)
      {
         try
         {
            Livros mdLivros = new Livros();

            mdLivros = _context.Livros.Where(l => l.id == id).FirstOrDefault();

            if (mdLivros == null)
            {
               return NotFound();
            }

            _context.Remove(mdLivros);
            _context.SaveChanges();

            return NoContent();
         }
         catch (Exception ex)
         {
            return Json(new { status = "error", message = "Erro ao excluir livro. Motivo:\n" + ex.Message });
         }
      }
   }
}
