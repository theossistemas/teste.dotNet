using TheosLivraria.Domain.Entidades;

namespace TheosLivraria.Domain.Interface
{
    public interface ILivroRepository : IRepositoryBase<int, Livro>
    {
        Task<bool> ExisteLivroComNome(string titulo);
    }
}
