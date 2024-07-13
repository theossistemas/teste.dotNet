using CatalogoLivros.DTOs;
using CatalogoLivros.Exceptions;
using CatalogoLivros.Models;
using CatalogoLivros.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoLivros.Controllers;

[Route("[controller]")]
[ApiController]
public class GenerosController : ControllerBase
{
    private readonly IGeneroRepository _repository;
    private readonly ILogger<GenerosController> _logger;

    public GenerosController(IGeneroRepository repository, ILogger<GenerosController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("livros")]
    [Authorize(Policy = "PublicAccess")]
    public ActionResult<IEnumerable<GeneroDTO>> GetGenerosLivros()
    {
        _logger.LogInformation("Iniciando a solicitação GET para obter todos os gêneros com seus livros.");
        var generosLivros = _repository.GetGenerosLivros();
        _logger.LogInformation("Solicitação GET para obter todos os gêneros com seus livros concluída com sucesso.");
        return Ok(generosLivros);
    }

    [HttpGet]
    [Authorize(Policy = "PublicAccess")]
    public ActionResult<IEnumerable<GeneroDTO>> Get()
    {
        _logger.LogInformation("Iniciando a solicitação GET para obter todos os gêneros.");
        var generos = _repository.GetGeneros();
        if (generos == null) 
        {
            return NotFound("Não existem categorias.");
        }

        var generosDto = new List<GeneroDTO>();
        foreach(var genero in generos)
        {
            var generoDto = new GeneroDTO()
            {
                Id = genero.Id,
                Nome = genero.Nome,
                Livros = genero.Livros
            };
            generosDto.Add(generoDto);
        }

        _logger.LogInformation("Solicitação GET para obter todos os gêneros concluída com sucesso.");
        return Ok(generosDto);
    }

    [HttpGet("{id:int}", Name = "ObterGenero")]
    [Authorize(Policy = "PublicAccess")]
    public ActionResult<GeneroDTO> Get(int id)
    {
        _logger.LogInformation("Iniciando a solicitação GET para obter o gênero com id={Id}.", id);
        var genero = _repository.GetGeneroPorId(id);
        if (genero is null)
        {
            _logger.LogWarning("Gênero com id={Id} não encontrado.", id);
            return NotFound($"Gênero com id={id} não encontrado.");
        }
        var generoDto = new GeneroDTO()
        {
            Id = genero.Id,
            Nome = genero.Nome,
            Livros = genero.Livros
        };

        _logger.LogInformation("Gênero com id={Id} encontrado com sucesso.", id);
        return Ok(generoDto);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public ActionResult<GeneroDTO> Post(GeneroDTO generoDto)
    {
        if (generoDto is null)
        {
            _logger.LogWarning("Tentativa de adicionar um gênero nulo.");
            return BadRequest("O gênero não pode ser nulo.");
        }
        _logger.LogInformation("Iniciando a solicitação POST para adicionar o gênero '{Nome}'.", generoDto.Nome);

        bool cadastroExiste = _repository.JaExisteCadastroGenero(generoDto.Nome);

        if(cadastroExiste)
        {
            _logger.LogWarning("O gênero '{Nome}' já está cadastrado no banco de dados.", generoDto.Nome);
            throw new JaExisteCadastroException("Gênero informado já cadastrado!");
        }

        var genero = new Genero()
        {
            Id = generoDto.Id,
            Nome = generoDto.Nome,
            Livros = generoDto.Livros
        };

        _repository.Create(genero);
        _logger.LogInformation("Novo gênero '{Nome}' adicionado com sucesso.", genero.Nome);
        
        return Ok(CreatedAtRoute("ObterGenero", new { id = genero.Id }, genero)); //rever
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public ActionResult<GeneroDTO> Put(int id, GeneroDTO generoDto)
    {
        if (id != generoDto.Id)
        {
            _logger.LogWarning("Tentativa de atualizar o gênero com id={Id} com dados inválidos.", id);
            return BadRequest("Dados inválidos");
        }
        _logger.LogInformation("Iniciando a solicitação PUT para atualizar o gênero com id={Id}.", id);

        var generoJaExistente = _repository.JaExisteCadastroGenero(generoDto.Nome);

        if (generoJaExistente)
        {
            _logger.LogWarning("O gênero '{Nome}' já está cadastrado no banco de dados.", generoDto.Nome);
            throw new JaExisteCadastroException("Gênero informado já cadastrado!");
        }

        var genero = new Genero()
        {
            Id = generoDto.Id,
            Nome = generoDto.Nome,
            Livros = generoDto.Livros
        };

        var generoAtualizado = _repository.Update(genero);
        _logger.LogInformation("Gênero com id={Id} atualizado com sucesso.", id);

        var generoAtualizadoDto = new GeneroDTO()
        {
            Id = generoAtualizado.Id,
            Nome = generoAtualizado.Nome,
            Livros = generoAtualizado.Livros
        };
        return Ok(generoAtualizadoDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public ActionResult<GeneroDTO> Delete(int id)
    {
        _logger.LogInformation("Iniciando a solicitação DELETE para remover o gênero com id={Id}.", id);

        var genero = _repository.Delete(id);
        _logger.LogInformation("Gênero com id={Id} removido com sucesso.", id);

        var generoExcluido = new GeneroDTO()
        {
            Id = genero.Id,
            Nome = genero.Nome,
            Livros = genero.Livros
        };
        return Ok(generoExcluido);
    }
}
