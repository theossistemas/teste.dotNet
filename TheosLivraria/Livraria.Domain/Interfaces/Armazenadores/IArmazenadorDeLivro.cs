using Livraria.Domain.Dto;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces.Armazenadores
{
    public interface IArmazenadorDeLivro
    {
        Task Armazenar(LivroDto dto);
    }
}
