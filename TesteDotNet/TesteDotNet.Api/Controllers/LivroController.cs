using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteDotNet.Api.ViewModel;
using TesteDotNet.Business.Interfaces;
using TesteDotNet.Business.Models;
using TesteDotNet.Data.Context;

namespace TesteDotNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LivroController : ControllerBase
    {
        private readonly DataDbContext _context;
        private readonly ILivroRepository _livroRespository;
        private readonly IMapper _mapper;

        public LivroController(DataDbContext context, ILivroRepository livroRespository, IMapper mapper)
        {
            _context = context;
            _livroRespository = livroRespository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        // GET: api/Livro
        [HttpGet]
        public async Task<IEnumerable<LivroViewModel>> GetLivros()
        {
            return _mapper.Map<IEnumerable<LivroViewModel>>(await _livroRespository.ListarPorNome());
        }

        // GET: api/Livro/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LivroViewModel>> GetLivro(Guid id)
        {
            return _mapper.Map<LivroViewModel>(await _livroRespository.ObterPorId(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(Guid id, LivroViewModel livroViewModel)
        {
            if (id != livroViewModel.Id)
            {
                return BadRequest();
            }

            var livroAtualizacao = _mapper.Map<LivroViewModel>(await _livroRespository.ObterPorId(id));

            livroAtualizacao.DataLancamento = livroViewModel.DataLancamento;
            livroAtualizacao.Nome = livroViewModel.Nome;
            livroAtualizacao.Categoria = livroViewModel.Categoria;
            livroAtualizacao.Autor = livroViewModel.Autor;
            livroAtualizacao.Edicao = livroViewModel.Edicao;

            await _livroRespository.Atualizar(_mapper.Map<Livro>(livroAtualizacao));
            return Ok(livroViewModel);

        }

        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(LivroViewModel livroViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _livroRespository.Adicionar(_mapper.Map<Livro>(livroViewModel));

            return Ok(livroViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Livro>> DeleteLivro(Guid id)
        {
            var livro = await _livroRespository.ObterPorId(id);

            if (livro == null) return NotFound();

            await _livroRespository.Remover(id);

            return Ok(livro);
        }

        private bool LivroExists(Guid id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}
