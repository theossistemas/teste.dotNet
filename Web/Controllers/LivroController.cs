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
    }
}
