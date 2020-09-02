using Livraria.Common.Model;
using Livraria.Domain.Dto;
using Livraria.Domain.Interfaces.Armazenadores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : BaseController
    {
        private readonly IArmazenadorDeLivro _armazenadorDeLivro;
        public LivroController(
            INotificationHandler<Notifications> notification,
            IArmazenadorDeLivro armazenadorDeLivro)
            : base(notification)
        {
            _armazenadorDeLivro = armazenadorDeLivro;
        }
        [HttpPost("AdicionarLivro")]
        public async Task<IActionResult> AdicionarLivro([FromBody] LivroDto dto)
        {
            await _armazenadorDeLivro.Armazenar(dto);

            if (!OperacaoValida())
                return BadRequestResponse();

            return Ok(true);
        }
    }
}
