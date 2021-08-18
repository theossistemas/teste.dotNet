using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteTheos.API.Models;
using TesteTheos.Data;
using TesteTheos.Data.QueryProviders;

namespace TesteTheos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<LivroViewModel>>> Get([FromServices] LivrosQueryProvider queryProvider, [FromServices]IMapper mapper, string nome, string autor, CancellationToken cancellationToken)
        {
            IQueryable<Livro> query = queryProvider.BaseQuery;

            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(q => q.Nome.StartsWith(nome));
            }

            if (!string.IsNullOrWhiteSpace(autor))
            {
                query = query.Where(q => q.Autor.StartsWith(autor));
            }

            var result = await mapper.ProjectTo<LivroViewModel>(query.OrderBy(q => q.Nome))
                .ToListAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LivroDetalhesViewModel>> GetById([FromServices] LivrosQueryProvider queryProvider, [FromServices] IMapper mapper, Guid id, CancellationToken cancellationToken)
        {
            await queryProvider.ThrowIfNotExistsAsync(id, cancellationToken);

            var result = await mapper.ProjectTo<LivroDetalhesViewModel>(queryProvider.GetByIdQuery(id))
                .FirstOrDefaultAsync(cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id:guid}/detalhes")]
        public async Task<ActionResult<Livro>> GetDetalhesById([FromServices] LivrosQueryProvider queryProvider, [FromServices] IMapper mapper, Guid id, CancellationToken cancellationToken)
        {
            await queryProvider.ThrowIfNotExistsAsync(queryProvider.GetByIdQuery(id).IgnoreQueryFilters(), cancellationToken);

            var result = await queryProvider.GetByIdQuery(id)
                .Include(q => q.CriadoPor)
                .Include(q => q.ModificadoPor)
                .Include(q => q.ExcluidoPor)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromServices] LivrosQueryProvider queryProvider, [FromServices] IMapper mapper, LivroModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await queryProvider.ThrowIfAlreadyExistsAsync(model.Nome, model.Autor, cancellationToken);

            var livro = mapper.Map<Livro>(model);
            livro.DataCriacao = DateTime.Now;
            livro.CriadoPorId = User.Identity.GetId();
            queryProvider.DataContext.Livros.Add(livro);
            await queryProvider.DataContext.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<LivroDetalhesViewModel>(livro);
            return Created(Url.Link(null, new { id = livro.Id }), result);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<LivroDetalhesViewModel>> Update([FromServices] LivrosQueryProvider queryProvider, [FromServices] IMapper mapper, Guid id, LivroModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await queryProvider.ThrowIfAlreadyExistsAsync(id, model.Nome, model.Autor, cancellationToken);

            var livro = queryProvider.GetByIdQuery(id).FirstOrDefault();
            mapper.Map(model, livro);
            livro.Id = id;
            livro.DataModificacao = DateTime.Now;
            livro.ModificadoPorId = User.Identity.GetId();
            queryProvider.DataContext.Update(livro);
            await queryProvider.DataContext.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<LivroDetalhesViewModel>(livro);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromServices] LivrosQueryProvider queryProvider, Guid id, CancellationToken cancellationToken)
        {
            await queryProvider.ThrowIfNotExistsAsync(id, cancellationToken);

            var livro = new Livro { Id = id };
            livro.ExcluidoPorId = User.Identity.GetId();
            queryProvider.DataContext.Remove(livro);
            await queryProvider.DataContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
