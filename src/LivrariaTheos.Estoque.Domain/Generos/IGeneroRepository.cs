using LivrariaTheos.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Domain.Generos
{
    public interface IGeneroRepository : IRepository<Genero>
    {
        Task<IEnumerable<Genero>> ObterTodosAtivos();
        Task<IEnumerable<Genero>> ObterTodos();
        Task<Genero> ObterPorId(int id);
        Task<IEnumerable<Genero>> ObterPorNome(string nome);

        void Adicionar(Genero genero);
        void Atualizar(Genero genero);
        void Excluir(Genero genero);
    }
}
