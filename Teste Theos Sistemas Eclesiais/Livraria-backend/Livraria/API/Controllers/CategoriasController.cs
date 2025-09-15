using Livraria.API.DTOs;
using Livraria.API.DTOs.Categorias;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriasController(ICategoriaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var categorias = await _service.ListarCategoriasAsync();
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Obter(Guid id)
    {
        // se não existir, o Service lança KeyNotFoundException → middleware retorna 404
        var categoria = await _service.ObterCategoriaPorIdAsync(id);
        return Ok(categoria);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Criar([FromBody] CategoriaRequest dto)
    {
        var categoria = new Categoria(dto.Nome);
        var resultado = await _service.CriarCategoriaAsync(categoria);
        // se já existir, Service lança CategoriaJaExisteException → middleware retorna 409
        return CreatedAtAction(nameof(Obter), new { id = resultado.Id }, resultado);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] CategoriaRequest dto)
    {
        var categoria = new Categoria(dto.Nome);
        typeof(Categoria).GetProperty("Id")?.SetValue(categoria, id);

        var atualizado = await _service.AtualizarCategoriaAsync(categoria);
        // se não existir, Service lança KeyNotFoundException → middleware retorna 404
        return Ok(atualizado);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        await _service.DeletarCategoriaAsync(id);
        // se não existir, Service lança KeyNotFoundException → middleware retorna 404
        return NoContent();
    }
}
