using System;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.Dto;
using BooksApi.Models;
using BooksApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILogger<LivroController> _logger;
        public readonly IRepositoryLivro _repo;
        private readonly IMapper _mapper;

        public LivroController(IRepositoryLivro repo, IMapper mapper, ILogger<LivroController> log)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = log;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLivros()
        {
            try
            {
                _logger.LogInformation(
                    $"Metodo GetAllLivros  : LivroController - > executou Dia :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");
                var livros = await _repo.getAllBookAsync();
                return Ok(livros);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error metdodo GetAllLivros :: {DateTime.Now.ToShortDateString()} :: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error ao retornar os livros, {e.Message}");
            }
        }

        [HttpGet("{livroId}")]
        public async Task<IActionResult> GetLivroToId(int livroId)
        {
            try
            {
                _logger.LogInformation(
                    $"Metodo GetLivroToId : LivroController - > executou Dia :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");

                var livro = await _repo.getBookByIdAsync(livroId);
                var result = _mapper.Map<LivroDto>(livro);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error metdodo Get Livro por ID :: {DateTime.Now.ToShortDateString()} :: {e.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error ao retornar os livros, {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterLivro(LivroDto model)
        {
            try
            {
                _logger.LogInformation(
                    $"Metodo RegisterLivro  : LivroController - > executou Dia :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");

                var exists = _repo.BookExist(model.Isbn);

                if (exists)
                {
                    _logger.LogInformation(
                        $"Metodo RegisterLivro  : LivroController - > executou Dia :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}:: Livro Duplicado ");
                    return StatusCode(StatusCodes.Status409Conflict, $"Já existe um livro com esse ISBN,{model.Isbn}");
                }

                var livro = _mapper.Map<Livro>(model);
                _repo.Add(livro);
                if (await _repo.SaveChangeAsync())
                {
                    _logger.LogInformation(
                        $"Metodo RegisterLivro  : LivroController - > Registrou Livro Isbn {model.Isbn} :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");

                    return Created($"v1/api/livro/{livro.Id}", _mapper.Map<LivroDto>(livro));
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error metdodo RegisterLivro :: {DateTime.Now.ToShortDateString()} :: {e.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error ao salvar,{e.Message}");
            }

            return BadRequest();
        }

        [HttpPut("{livroId}")]
        public async Task<IActionResult> AtualizarLivro(int livroId, LivroDto model)
        {
            try
            {
                _logger.LogInformation(
                    $"Metodo AtualizarLivro  : LivroController - > executou Dia :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");

                var livro = await _repo.getBookByIdAsync(livroId);
                if (livro == null) return NotFound();
                _mapper.Map(model, livro);
                _repo.Update(livro);

                if (await _repo.SaveChangeAsync())
                {
                    _logger.LogInformation(
                        $"Metodo AtualizarLivro  : LivroController - > Livro ISBN {model.Isbn} atualizado :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");

                    return Created($"v1/api/livro/{livro.Id}", _mapper.Map<LivroDto>(model));
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error metdodo AtualizarLivro Isbn {model.Isbn} :: {DateTime.Now.ToShortDateString()} :: {e.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, "Atualição de dados Falhou");
            }

            return BadRequest();
        }

        [HttpDelete("{livroId}")]
        public async Task<IActionResult> ExcluirLivro(int livroId)
        {
            try
            {
                _logger.LogInformation(
                    $"Metodo ExcluirLivro  : LivroController - > executou Dia :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");
                var result = await _repo.getBookByIdAsync(livroId);
                if (result == null) return NotFound();

                _repo.Delete(result);
                if (await _repo.SaveChangeAsync())
                {
                    _logger.LogInformation(
                        $"Metodo ExcluirLivro  : LivroController - > Livro Isbn {result.Isbn} :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");

                    return Ok();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error metdodo ExcluirLivro :: {DateTime.Now.ToShortDateString()} :: {e.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, "Impossível executar essa operação agora!");
            }
            _logger.LogWarning($"metdodo ExcluirLivro :: ${BadRequest()} {DateTime.Now.ToShortDateString()}");

            return BadRequest();
        }
    }
}