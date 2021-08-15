using LivrariaTheos.Estoque.Domain.Dtos;
using LivrariaTheos.Estoque.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LivrariaTheos.Estoque.Domain.Livros;
using System.IO;
using System.Linq;
using LivrariaTheos.Estoque.Domain.Logs;
using LivrariaTheos.Estoque.Domain.Livros.Dto;

namespace LivrariaTheos.WebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : BaseController
    {
        private readonly ILivroAppService _livroAppService;
        private readonly IMapper _mapper;
        private readonly ArmazenadorDeLivro _armazenadorDeLivro;       

        public LivroController(ILivroAppService livroAppService,
            IMapper mapper,
            ArmazenadorDeLivro armazenadorDeLivro,
            ArmazenadorDeLogAplicacao armazenadorDeLogAplicacao) : base(armazenadorDeLogAplicacao)
        {
            _livroAppService = livroAppService;
            _mapper = mapper;
            _armazenadorDeLivro = armazenadorDeLivro;
            
        }

        [HttpGet("ObterLivro/{id}")]
        public async Task<ActionResult<LivroDtoRetorno>> ObterLivro(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _livroAppService.ObterPorId(id);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterPorNome/{nome}")]
        public async Task<ActionResult<IEnumerable<LivroDtoRetorno>>> ObterPorNome(string nome)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _livroAppService.ObterPorNome(nome);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterLivrosAtivos")]
        public async Task<ActionResult<IEnumerable<LivroDtoRetorno>>> ObterLivrosAtivos()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _livroAppService.ObterTodosAtivos();

                return Ok(resultado.OrderBy(l => l.Nome));
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterLivros")]
        public async Task<ActionResult<IEnumerable<LivroDtoRetorno>>> ObterLivros()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _livroAppService.ObterTodos();

                return Ok(resultado.OrderBy(l =>l.Nome));
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterPorAutor/{autorId}")]
        public async Task<ActionResult<IEnumerable<LivroDtoRetorno>>> ObterPorAutor(int autorId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _livroAppService.ObterPorAutor(autorId);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterPorGenero/{generoId}")]
        public async Task<ActionResult<IEnumerable<LivroDtoRetorno>>> ObterPorGenero(int generoId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _livroAppService.ObterPorGenero(generoId);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet("ObterPorNacionalidadeDoAutor/{nacionalidadeId}")]
        public async Task<ActionResult<IEnumerable<LivroDtoRetorno>>> ObterPorNacionalidadeDoAutor(int nacionalidadeId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = await _livroAppService.ObterPorNacionalidadeDoAutor(nacionalidadeId);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarLivro(int id, LivroDto livroDto)
        {
            if (id != livroDto.Id)
            {
                return BadRequest();
            }

            var Livro = await _livroAppService.ObterPorId(id);

            if (Livro == null)
            {
                return NotFound();
            }

            try
            {
                var resultado = await _armazenadorDeLivro.Armazenar(_mapper.Map<Livro>(livroDto));

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<LivroDto>> CriarLivro(LivroDto livroDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var resultado = await _armazenadorDeLivro.Armazenar(_mapper.Map<Livro>(livroDto));

                return Ok(_mapper.Map<LivroDto>(resultado));
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirLivro(int id)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _armazenadorDeLivro.Excluir(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return await ErroComLogAsync(ex);
            }
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("ObterCapa/{caminho}/{nomeArquivo}")]
        public IActionResult ObterCapa(string caminho, string nomeArquivo)
        {
            try
            {
                var folderName = Path.Combine("StaticFiles", "Capas");
                var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), caminho);
                var photos = Directory.EnumerateFiles(pathToRead)
                    .Where(ImagemEmFormatoValido)
                    .Select(fullPath => Path.Combine(caminho,nomeArquivo)).First();

                return Ok(new { photos });

            }
            catch (Exception ex)
            {
                return BadRequest($"Não possivel processar a sua solicitação. {ex}");
            }
        }

        private bool ImagemEmFormatoValido(string nomeArquivo)
        {
            return nomeArquivo.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                || nomeArquivo.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                || nomeArquivo.EndsWith(".png", StringComparison.OrdinalIgnoreCase);
        }
    }
}