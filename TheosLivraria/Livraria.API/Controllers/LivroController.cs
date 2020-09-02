using Livraria.Common.Model;
using Livraria.Domain.Dto;
using Livraria.Domain.Interfaces.Armazenadores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : BaseController
    {
        private readonly IArmazenadorDeLivro _armazenadorDeLivro;
        private readonly ILogger _logger;
        public LivroController(
            INotificationHandler<Notifications> notification,
            ILogger<LivroController> logger,
            IArmazenadorDeLivro armazenadorDeLivro)
            : base(notification)
        {
            _armazenadorDeLivro = armazenadorDeLivro;
            _logger = logger;
        }
        [HttpPost("AdicionarLivro")]
        public async Task<IActionResult> AdicionarLivro([FromBody] LivroDto dto)
        {
            //_logger.LogInformation(1002, "Adicionar Livro Controller");
            await _armazenadorDeLivro.Armazenar(dto);

            if (!OperacaoValida())
                return BadRequestResponse();

            return Ok(true);
        }
    }
}
