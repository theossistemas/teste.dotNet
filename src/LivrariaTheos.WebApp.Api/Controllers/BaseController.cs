using LivrariaTheos.Estoque.Domain.Logs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaTheos.WebApp.Api.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ArmazenadorDeLogAplicacao _armazenadorDeLogAplicacao;

        protected BaseController(ArmazenadorDeLogAplicacao armazenadorDeLogAplicacao)
        {
            _armazenadorDeLogAplicacao = armazenadorDeLogAplicacao;       
        }

        protected async Task<BadRequestObjectResult> ErroComLogAsync(Exception ex)
        {
            try
            {
                var logAplicacao = new LogAplicacao(ex.Source, ex.Message, ex.StackTrace);

                 await _armazenadorDeLogAplicacao.Armazenar(logAplicacao);

            }
            catch { }            

            return BadRequest(ex.Message);
        }
    }
}