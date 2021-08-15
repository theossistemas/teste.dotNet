using LivrariaTheos.Estoque.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Application.Services
{
    public interface IAutorAppService : IDisposable
    {
        Task<IEnumerable<AutorDto>> ObterTodosAtivos();
        Task<IEnumerable<AutorDto>> ObterTodos();
        Task<AutorDto> ObterPorId(int id);
        Task<IEnumerable<AutorDto>> ObterPorNome(string nome);
        Task<IEnumerable<AutorDto>> ObterPorNacionalidade(int nacionalidade);      
    }
}