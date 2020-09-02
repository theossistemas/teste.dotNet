using Livraria.Common.Model;
using Livraria.Common.Utils;
using Livraria.Domain.Dto;
using Livraria.Domain.Interfaces.Armazenadores;
using Livraria.Domain.Interfaces.Removedores;
using Livraria.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : BaseController
    {
        private readonly IArmazenadorDeLivro _armazenadorDeLivro;
        private readonly IRemovedorDeLivro _removedorDeLivro;
        private readonly ILivroRepositorio _livroRepositorio;
        private readonly ILogger _logger;
        public LivroController(
            INotificationHandler<Notifications> notification,
            ILogger<LivroController> logger,
            IArmazenadorDeLivro armazenadorDeLivro,
            IRemovedorDeLivro removedorDeLivro,
            ILivroRepositorio livroRepositorio)
            : base(notification)
        {
            _armazenadorDeLivro = armazenadorDeLivro;
            _removedorDeLivro = removedorDeLivro;
            _livroRepositorio = livroRepositorio;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            await _removedorDeLivro.Remover(id);
            if (!OperacaoValida())
                return BadRequestResponse();

            return Ok(true);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var livro = await _livroRepositorio.ObterPorId(id);
                return Ok(LivroDto.ConverterParaDto(livro));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resources.MSG_Status500);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var resultados = await _livroRepositorio.ObterTodosOrdenadoPorNome();
                return Ok(resultados.Select( x => LivroDto.ConverterParaDto(x)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resources.MSG_Status500);
            }
        }

    }
}
