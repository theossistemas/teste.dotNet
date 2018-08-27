using Livraria.Command.Notifications;
using Livraria.Service.DTOs;
using Livraria.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService, INotificationHandler<Notification> notifications) : base(notifications)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("[action]")]
        public IActionResult Autenticar([FromBody]UsuarioDTO usuarioDTO)
        {
            return Response(_usuarioService.Autenticar(usuarioDTO.Login, usuarioDTO.Senha));
        }
    }
}