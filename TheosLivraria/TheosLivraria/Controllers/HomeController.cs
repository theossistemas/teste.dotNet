using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.Models;

namespace TheosLivraria.Controllers
{
    /// <summary>
    /// HomeController
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Página Principal da Aplicação
        /// </summary>
        /// <returns></returns>
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Página com informações do candidato à vaga BackEnd C# da Theòs, Felipe Rampazzo Farias
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Sobre")]
        public IActionResult Sobre()
        {
            return View();
        }

        [HttpGet("/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
