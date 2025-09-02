using Livraria.Domain.Entities;

namespace Livraria.Application.Interfaces
{
    public interface ILivro
    {
        Task<IEnumerable<Livro>> GetAllAsync();
        Task<Livro?> GetByIdAsync(int id);
        Task<Livro?> GetByIsbnAsync(string isbn);
        Task AddAsync(Livro livro);
        Task UpdateAsync(Livro livro);
        Task DeleteAsync(int id);
    }
}
