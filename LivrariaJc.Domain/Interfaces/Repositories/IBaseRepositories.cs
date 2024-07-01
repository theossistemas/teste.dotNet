using LivrariaJc.Domain.Entidades;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaJc.Domain.Interface.Repositories
{
    public interface IBaseRepositories<T> where T : BaseEntidade
    {
        Task<T> InserirAsync(T item);
        Task<T> AtualizarAsync(T item);
        Task<bool> ExcluirAsync(int id);
        Task<T> SelecionarAsync(int id);
        Task<IQueryable<T>> SelecionarTodosAsync();
        Task<bool> ExisteAsync(int id);
        int UltimoId();
    }
}
