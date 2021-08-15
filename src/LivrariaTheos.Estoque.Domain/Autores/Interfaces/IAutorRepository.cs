using LivrariaTheos.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Domain.Autores.Interfaces
{
    public interface IAutorRepository : IRepository<Autor>
    {
        Task<Autor> ObterPorId(int id);
        Task<IEnumerable<Autor>> ObterTodosAtivos();
        Task<IEnumerable<Autor>> ObterTodos();
        Task<IEnumerable<Autor>> ObterPorNacionalidade(int nacionalidade);
        Task<IEnumerable<Autor>> ObterPorNome(string nome);

        void Adicionar(Autor autor);
        void Atualizar(Autor autor);
        void Excluir(Autor autor);
    }
}