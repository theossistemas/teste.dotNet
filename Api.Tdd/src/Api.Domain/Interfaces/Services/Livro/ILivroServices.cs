using Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.CategoriaQuarto
{
    public interface ILivroServices
    {
        Task<LivroDto> Get(int id);
        Task<IEnumerable<LivroDto>> GetAll();        
        Task<LivroDto> Create(LivroDto categoria);
        Task<LivroDto> Update(LivroDto categoria);
        Task<bool> Delete(int id);
    }
}
