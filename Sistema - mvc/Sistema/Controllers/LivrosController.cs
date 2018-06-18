using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema.Models;

namespace Sistema.Controllers
{
   public class LivrosController : Controller
   {
      private readonly SistemaContext _context;

      public LivrosController(SistemaContext context)
      {
         _context = context;
      }

      // GET: Livros
      public async Task<IActionResult> Index(string sortOrder, string currentFilter,
                                  string searchString, int? page)
      {
         var livros = from s in _context.Livros select s;

         ViewData["DescricaoParm"] = String.IsNullOrEmpty(sortOrder) ? "descricao_desc" : "";

         if (searchString != null)
            page = 1;
         else
            searchString = currentFilter;

         ViewData["CurrentFilter"] = searchString;
         ViewData["CurrentSort"] = sortOrder;

         if (!String.IsNullOrEmpty(searchString))
         {
            livros = livros.Where(c => c.Descricao.Contains(searchString));
         }

         if (sortOrder == "descricao_desc")
            livros = livros.OrderByDescending(c => c.Descricao);
         else
            livros = livros.OrderBy(c => c.Descricao);

         int pageSize = 3;

         return View(await Paginacao<Livros>.CreateAsync(livros.AsNoTracking(), page ?? 1, pageSize));
      }

      // GET: Livros/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var livros = await _context.Livros
             .SingleOrDefaultAsync(m => m.id == id);
         if (livros == null)
         {
            return NotFound();
         }

         return View(livros);
      }

      // GET: Livros/Create
      public IActionResult Create()
      {
         return View();
      }

      // POST: Livros/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("id,Descricao,valor")] Livros livros)
      {
         if (ModelState.IsValid)
         {
            _context.Add(livros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         return View(livros);
      }

      // GET: Livros/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var livros = await _context.Livros.SingleOrDefaultAsync(m => m.id == id);
         if (livros == null)
         {
            return NotFound();
         }
         return View(livros);
      }

      // POST: Livros/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("id,Descricao,valor")] Livros livros)
      {
         if (id != livros.id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(livros);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!LivrosExists(livros.id))
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
         return View(livros);
      }

      // GET: Livros/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var livros = await _context.Livros
             .SingleOrDefaultAsync(m => m.id == id);
         if (livros == null)
         {
            return NotFound();
         }

         return View(livros);
      }

      // POST: Livros/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var livros = await _context.Livros.SingleOrDefaultAsync(m => m.id == id);
         _context.Livros.Remove(livros);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool LivrosExists(int id)
      {
         return _context.Livros.Any(e => e.id == id);
      }
   }
}
