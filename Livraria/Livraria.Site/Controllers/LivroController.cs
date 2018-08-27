using Livraria.Command.Notifications;
using Livraria.Service.DTOs;
using Livraria.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Site.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ApiController
    {
        private readonly ILivroService _livroService;
        public LivroController(ILivroService livroService, INotificationHandler<Notification> notifications) : base(notifications)
        {
            _livroService = livroService;
        }

        [HttpPost("[action]")]
        public IActionResult Incluir([FromBody]LivroDTO livroDTO)
        {
            _livroService.Incluir(livroDTO);
            return Response();
        }

        [HttpPut("[action]")]
        public IActionResult Alterar([FromBody]LivroDTO livroDTO)
        {
            _livroService.Alterar(livroDTO);
            return Response();
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult Excluir(string id)
        {
            _livroService.Excluir(id);
            return Response();
        }
        [AllowAnonymous]
        [HttpGet("[action]/{id}")]
        public IActionResult Consultar(string id)
        {
            return Response(_livroService.Consultar(id));
        }
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult Listar()
        {
            return Response(_livroService.Listar());
        }
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult ListarOrdenadoPorNome()
        {
            return Response(_livroService.ListarOrdenadoPorNome());
        }
    }
}