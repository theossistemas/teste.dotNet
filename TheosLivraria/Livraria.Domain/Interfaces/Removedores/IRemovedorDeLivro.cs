using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces.Removedores
{
    public interface IRemovedorDeLivro
    {
        Task Remover(int id);
    }
}
