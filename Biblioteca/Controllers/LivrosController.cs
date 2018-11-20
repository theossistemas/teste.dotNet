using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Data;
using Biblioteca.Models.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteca.Controllers
{
    /// <summary>
    /// Controller Livros
    /// </summary>       
    public class LivrosController : Controller
    {
        private readonly Contexto _context;

        /// <summary>
        /// Construtor
        /// </summary>   
        /// <param name="context">Contexto da aplicação</param>        
        public LivrosController(Contexto context)
        {
            _context = context;
        }

        // GET: Livros
        /// <summary>
        /// Lista de Livros
        /// </summary>       
        /// <returns>lista de livros para a View</returns>        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Livro.OrderBy(l => l.LivroNome).ToListAsync());
        }

        // GET: Livros/Details/5
        /// <summary>
        /// envia para a tela de livro detalhado
        /// </summary>       
        /// <param name="id">Id do livro</param>        
        /// <returns>Retorna a View com os detalhes do livro</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .FirstOrDefaultAsync(m => m.LivroId == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        /// <summary>
        /// Envia para a tela de Cadastro do livro
        /// </summary>       
        /// <returns>Retorna a View de cadastro</returns>
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Action Post de cadastro.
        /// </summary>       
        /// <param name="Livro">Entidade Livro</param>       
        /// <returns>Retorna para a view de listagem, caso obtenha sucesso no cadastro</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<IActionResult> Create([Bind("LivroId,LivroNome,LivroAutor,LivroDataPublicacao")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(livro);
        }

        // GET: Livros/Edit/5
        /// <summary>
        /// Envia para a tela de Edição do livro
        /// </summary>       
        /// <returns>Retorna a View de edição</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Action Post de alteração.
        /// </summary>       
        /// <param name="livro">Entidade Livro</param>       
        /// <param name="LivroId">Entidade Livro</param>       
        /// <returns>Retorna para a view de listagem, caso obtenha sucesso na alteração</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int LivroId, [Bind("LivroId,LivroNome,LivroAutor,LivroDataPublicacao")] Livro livro)
        {
            if (LivroId != livro.LivroId)
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
                    if (!LivroExists(livro.LivroId))
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

        // GET: Livros/Delete/5
        /// <summary>
        /// Envia para a tela de Deleção do livro
        /// </summary>       
        /// <param name="id">Id do livro</param>       
        /// <returns>Retorna a View de deleção</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .FirstOrDefaultAsync(m => m.LivroId == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        /// <summary>
        /// Action Post de deleção.
        /// </summary>       
        /// <param name="LivroId">Id do Livro</param>       
        /// <returns>Retorna para a view de listagem, caso obtenha sucesso na deleção</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int LivroId)
        {
            var livro = await _context.Livro.FindAsync(LivroId);
            _context.Livro.Remove(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
            return _context.Livro.Any(e => e.LivroId == id);
        }
    }
}
