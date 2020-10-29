using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace LivrariaTheos.WebApp.Controllers
{
    public class LivrosController : Controller
    {
        #region Properties
        private readonly IService<Livros> _service;
        #endregion

        #region Constructor
        public LivrosController(IService<Livros> service)
        {
            _service = service;
        }
        #endregion

        #region Methods
        public async Task<ActionResult<IEnumerable<Livros>>> Index()
        {
            return View(await _service.FindAllAsync());
        }

        public async Task<ActionResult<Livros>> Details(int id)
        {
            return View(await _service.FindAsync(id));
        }

        [Authorize]
        public ActionResult Create()
        {
            return View(new Livros { DataAquisicao = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Livros>> Create([FromForm] Livros model)
        {
            if (ModelState.IsValid)
            {
                if (!BuscarLivroRegistrado(model))
                {
                    await _service.InsertAsync(model);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Existe = "Livro já cadastrado";
            }

            return View();
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            return View(_service.FindAsync(id).Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Livros model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.UpdateChangesAsync(model);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ViewBag.Exist = "Revise as informações!";
            }
            return View();
        }

        [Authorize]
        public async Task<ActionResult<Livros>> Delete(int id)
        {
            await _service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Private methods
        private bool BuscarLivroRegistrado(Livros model)
        {
            var list = _service.FindAllAsync().Result;
            foreach (var item in list)
            {
                if (item.NomeLivro == model.NomeLivro)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
