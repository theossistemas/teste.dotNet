using Livraria.Domain.Entities.Cadastros;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Interfaces.Repositories.Cadastros
{
    public interface ILivroRepositorio : IGenericRepository<Livro>
    {
        Task DeleteAll();
        Task<Livro> GetByTituloEGenero(string titulo, int generoId);
    }
}
