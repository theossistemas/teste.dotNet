using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.DTO;
using RestAPIClient.Livros;
using RestAPIClient.Response;
using Web.Models;

namespace Web.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILogger<LivroController> _logger;

        private ILivroClient livroClient;

        public LivroController(ILogger<LivroController> logger, ILivroClient livroClient)
        {
            _logger = logger;

            this.livroClient = livroClient;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IApiResponse response = await livroClient.FindAll();

            ViewBag.Livros = response.ResponseBody as IList<LivroDTO>;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(Int64? id)
        {
            IApiResponse response = await livroClient.Delete(id, Sessao.Usuario);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.NoContent)
            {
                ViewBag.Message = "Excluído com sucesso !";

                return View("Livro", new LivroDTO());
            }

            ViewBag.ErrorMessage = response.ErrorMessage;

            return View("Livro", new LivroDTO());
        }
    }
}
