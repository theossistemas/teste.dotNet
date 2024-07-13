using CatalogoLivros.DTOs;
using CatalogoLivros.Exceptions;
using CatalogoLivros.Models;
using CatalogoLivros.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoLivros.Controllers;

[Route("[controller]")]
[ApiController]
public class LivrosController : ControllerBase
{
    private readonly ILivroRepository _repository;
    private readonly ILogger<LivrosController> _logger;

    public LivrosController(ILivroRepository repository, ILogger<LivrosController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Policy = "PublicAccess")]
    public ActionResult<IEnumerable<LivroDTO>> Get()
    {
        _logger.LogInformation("Iniciando a solicitação GET para obter todos os livros.");
        var livros = _repository.GetLivros();
        if(livros == null)
        {
            return NotFound("Não existem livros cadastrados.");
        }

        var livrosDto = new List<LivroDTO>();
        foreach (var livro in livros)
        {
            var livroDto = new LivroDTO()
            {
                Id = livro.Id,
                Nome = livro.Nome,
                Autor = livro.Autor,
                Ano = livro.Ano,
                Editora = livro.Editora,
                DataCadastro = livro.DataCadastro,
                QtdEstoque = livro.QtdEstoque,
                GeneroId = livro.GeneroId
            };
            livrosDto.Add(livroDto);
        }

        _logger.LogInformation("Solicitação GET para obter todos os livros concluída com sucesso.");
        return Ok(livrosDto);
    }

    [HttpGet("{id:int}", Name = "ObterLivro")]
    [Authorize(Policy = "PublicAccess")]
    public ActionResult<LivroDTO> Get(int id)
    {
        _logger.LogInformation("Iniciando a solicitação GET para obter o livro com id={Id}.", id); //rever
        var livro = _repository.GetLivroPorId(id);
        if (livro is null)
        {
            _logger.LogWarning("Livro com id={Id} não encontrado.", id);
            return NotFound($"Livro com id={id} não encontrado.");
        }
        var livroDto = new LivroDTO()
        {
            Id = livro.Id,
            Nome = livro.Nome,
            Autor = livro.Autor,
            Ano = livro.Ano,
            Editora = livro.Editora,
            DataCadastro = livro.DataCadastro,
            QtdEstoque = livro.QtdEstoque,
            GeneroId = livro.GeneroId
        };

        _logger.LogInformation("Livro com id={Id} encontrado com sucesso.", id);
        return Ok(livroDto);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public ActionResult<LivroDTO> Post(LivroDTO livroDto)
    {
        if (livroDto is null)
        {
            _logger.LogWarning("Tentativa de adicionar um livro nulo.");
            return BadRequest();
        }
        _logger.LogInformation("Iniciando a solicitação POST para adicionar o livro '{Nome}' de '{Autor}'.", livroDto.Nome, livroDto.Autor);

        bool livroJaExistente = _repository.JaExisteCadastroLivro(livroDto.Nome, livroDto.Autor, livroDto.Ano);

        if (livroJaExistente)
        {
            _logger.LogWarning("O livro '{Nome}' de '{Autor}' já está cadastrado no banco de dados.", livroDto.Nome, livroDto.Autor);
            throw new JaExisteCadastroException("Livro informado já cadastrado!");
        }
        var livro = new Livro()
        {
            Id = livroDto.Id,
            Nome = livroDto.Nome,
            Autor = livroDto.Autor,
            Ano = livroDto.Ano,
            Editora = livroDto.Editora,
            DataCadastro = livroDto.DataCadastro,
            QtdEstoque = livroDto.QtdEstoque,
            GeneroId = livroDto.GeneroId
        };

        _repository.Create(livro);
        _logger.LogInformation("Novo livro '{Nome}' de '{Autor}' adicionado com sucesso.", livro.Nome, livro.Autor);

        return Ok(CreatedAtRoute("ObterLivro", new { id = livro.Id }, livro));
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public ActionResult<LivroDTO> Put(int id, LivroDTO livroDto)
    {
        if (id != livroDto.Id)
        {
            _logger.LogWarning("Tentativa de atualizar o livro com id={Id} com dados inválidos.", id);
            return BadRequest("Dados inválidos");
        }
        _logger.LogInformation("Iniciando a solicitação PUT para atualizar o livro com id={Id}.", id);

        var livroJaExistente = _repository.JaExisteCadastroLivro(livroDto.Nome, livroDto.Autor, livroDto.Ano);

        if (livroJaExistente)
        {
            _logger.LogWarning("O livro '{Nome}' de '{Autor}' já está cadastrado no banco de dados.", livroDto.Nome, livroDto.Autor);
            throw new JaExisteCadastroException("Livro informado já cadastrado!");
        }
        var livro = new Livro()
        {
            Id = livroDto.Id,
            Nome = livroDto.Nome,
            Autor = livroDto.Autor,
            Ano = livroDto.Ano,
            Editora = livroDto.Editora,
            DataCadastro = livroDto.DataCadastro,
            QtdEstoque = livroDto.QtdEstoque,
            GeneroId = livroDto.GeneroId
        };

        var livroAtualizado = _repository.Update(livro);
        _logger.LogInformation("Livro com id={Id} atualizado com sucesso.", id);

        var livroAtualizadoDto = new LivroDTO()
        {
            Id = livroAtualizado.Id,
            Nome = livroAtualizado.Nome,
            Autor = livroAtualizado.Autor,
            Ano = livroAtualizado.Ano,
            Editora = livroAtualizado.Editora,
            DataCadastro = livroAtualizado.DataCadastro,
            QtdEstoque = livroAtualizado.QtdEstoque,
            GeneroId = livroAtualizado.GeneroId
        };
        return Ok(livroAtualizadoDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public ActionResult<LivroDTO> Delete(int id)
    {
        _logger.LogInformation("Iniciando a solicitação DELETE para remover o livro com id={Id}.", id);

        var livro = _repository.Delete(id);
        _logger.LogInformation("Livro com id={Id} removido com sucesso.", id);

        var livroExcluido = new LivroDTO()
        {
            Id = livro.Id,
            Nome = livro.Nome,
            Autor = livro.Autor,
            Ano = livro.Ano,
            Editora = livro.Editora,
            DataCadastro = livro.DataCadastro,
            QtdEstoque = livro.QtdEstoque,
            GeneroId = livro.GeneroId
        };
        return Ok(livro);
    }
}
