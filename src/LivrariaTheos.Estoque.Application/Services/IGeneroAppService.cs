using LivrariaTheos.Estoque.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Application.Services
{
    public interface IGeneroAppService : IDisposable
    {    
        Task<GeneroDto> ObterPorId(int id);
        Task<IEnumerable<GeneroDto>> ObterPorNome(string nome);
        Task<IEnumerable<GeneroDto>> ObterTodos();
        Task<IEnumerable<GeneroDto>> ObterTodosAtivos();
    }
}