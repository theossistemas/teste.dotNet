using Livraria.Domain.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces.Repository
{
    public interface ILivroRepositorio : IRepository<Livro>
    {
        Task<List<Livro>> ObterTodosOrdenadoPorNome();
        Task<Livro> ObterPorTitulo(string titulo);
        Task<Livro> ObterPorId(int id);
    }
}
