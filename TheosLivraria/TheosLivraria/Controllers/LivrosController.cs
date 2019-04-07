using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheosLivraria.Models;
using TheosLivraria.Models.Contexts;

namespace TheosLivraria.Controllers
{
    /// <summary>
    /// LivrosController
    /// </summary>
    public class LivrosController : Controller
    {
        private readonly LivroContext _context;

        public LivrosController(LivroContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Traz a view da listagem dos livros, por ordem alfabética.
        /// </summary>
        /// <returns></returns>
        // GET: Livros
        [AllowAnonymous]
        [HttpGet("/Livros/Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Livros.OrderBy(livro => livro.NomeLivro).ToListAsync());
        }

        /// <summary>
        /// Traz a view com os detalhes do livro selecionado.
        /// </summary>
        /// <param name="id">Id do livro</param>
        /// <returns></returns>
        // GET: Livros/Details/5
        [AllowAnonymous]
        [HttpGet("/Livros/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        /// <summary>
        /// Traz a view de criação de um novo livro.
        /// </summary>
        /// <returns></returns>
        // GET: Livros/Create
        [AllowAnonymous]
        [HttpGet("/Livros/Create")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Cria um novo Livro.
        /// </summary>
        /// <param name="livro"></param>
        /// <returns></returns>
        // POST: Livros/Create
        [AllowAnonymous]
        [HttpPost("/Livros/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ISBN,NomeLivro,NomeAutor,Editora,AnoLancamento,Edicao")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(livro);
        }

        /// <summary>
        /// Traz a view de edição de livro.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Livros/Edit/5
        [AllowAnonymous]
        [HttpGet("/Livros/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        /// <summary>
        /// Permite editar os dados de um livro existente.
        /// </summary>
        /// <param name="id">Id do livro</param>
        /// <param name="livro">Model do livro</param>
        /// <returns></returns>
        // POST: Livros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost("/Livros/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ISBN,NomeLivro,NomeAutor,Editora,AnoLancamento,Edicao")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(livro);
        }

        /// <summary>
        /// Traz a view de exclusão de livros.
        /// </summary>
        /// <param name="id">Id do livro</param>
        /// <returns></returns>
        // GET: Livros/Delete/5
        [AllowAnonymous]
        [HttpGet("/Livros/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        /// <summary>
        /// Deleta de fato o livro selecionado.
        /// </summary>
        /// <param name="id">Id do livro a ser deletado.</param>
        /// <returns></returns>
        // POST: Livros/Delete/5
        [AllowAnonymous]
        [HttpPost("/Livros/Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}
