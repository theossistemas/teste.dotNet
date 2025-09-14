using LivrariaTheos.Api.Contracts;
using LivrariaTheos.Domain.Entities;
using LivrariaTheos.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LivrariaTheos.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // /api/livros
public class LivrosController : ControllerBase
{
    private readonly LivrariaTheosDbContext _db;
    public LivrosController(LivrariaTheosDbContext db) => _db = db;

    // Público: listagem ordenada por Título ASC + busca + paginação
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] string? busca, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var q = _db.Livros.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            var s = busca.Trim();
            q = q.Where(l => l.Titulo.Contains(s) || l.Autor.Contains(s));
        }

        var total = await q.CountAsync();
        var itens = await q.OrderBy(l => l.Titulo)
                           .Skip((page - 1) * pageSize)
                           .Take(pageSize)
                           .ToListAsync();

        return Ok(new { total, page, pageSize, itens });
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
        => (await _db.Livros.FindAsync(id)) is { } livro ? Ok(livro) : NotFound();

    // Admin: criar
    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create([FromBody] CriarLivroDto dto)
    {
        var existe = await _db.Livros.AnyAsync(l =>
            l.Titulo == dto.Titulo &&
            l.Autor == dto.Autor &&
            l.AnoPublicacao == dto.AnoPublicacao);

        if (existe) return Conflict(new { message = "Livro já cadastrado (Título+Autor+Ano)." });

        var livro = new Livro(
            dto.Titulo, dto.Autor, dto.AnoPublicacao,
            dto.ISBN, dto.Editora, dto.DataPublicacao,
            dto.Preco, dto.Estoque
        );

        _db.Livros.Add(livro);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = livro.Id }, livro);
    }

    // Admin: atualizar
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AtualizarLivroDto dto)
    {
        var livro = await _db.Livros.FindAsync(id);
        if (livro is null) return NotFound();

        var duplicado = await _db.Livros.AnyAsync(l =>
            l.Id != id &&
            l.Titulo == dto.Titulo &&
            l.Autor == dto.Autor &&
            l.AnoPublicacao == dto.AnoPublicacao);

        if (duplicado) return Conflict(new { message = "Livro já cadastrado (Título+Autor+Ano)." });

        livro.Update(
            dto.Titulo, dto.Autor, dto.AnoPublicacao,
            dto.ISBN, dto.Editora, dto.DataPublicacao,
            dto.Preco, dto.Estoque
        );

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // Admin: excluir
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var livro = await _db.Livros.FindAsync(id);
        if (livro is null) return NotFound();

        _db.Livros.Remove(livro);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
