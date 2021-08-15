using LivrariaTheos.Estoque.Domain.Dtos;
using LivrariaTheos.Estoque.Domain.Livros.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Application.Services
{
    public interface ILivroAppService : IDisposable
    {
        Task<LivroDtoRetorno> ObterPorId(int id);
        Task<IEnumerable<LivroDtoRetorno>> ObterPorNome(string nome);
        Task<IEnumerable<LivroDtoRetorno>> ObterTodosAtivos();
        Task<IEnumerable<LivroDtoRetorno>> ObterTodos();
        Task<IEnumerable<LivroDtoRetorno>> ObterPorGenero(int generoId);
        Task<IEnumerable<LivroDtoRetorno>> ObterPorAutor(int autorId);
        Task<IEnumerable<LivroDtoRetorno>> ObterPorNacionalidadeDoAutor(int nacionalidade);        
    }
}