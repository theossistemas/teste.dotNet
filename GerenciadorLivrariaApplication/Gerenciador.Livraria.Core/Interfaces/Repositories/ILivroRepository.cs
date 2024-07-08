using Gerenciador.Livraria.Core.Entities.Livraria;

namespace Gerenciador.Livraria.Core.Interfaces.Repositories
{
    public interface ILivroRepository
    {
        Task<IEnumerable<LivroEntity>> GetAllIncluded();
    }
}