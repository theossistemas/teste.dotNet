using TheosLivraria.Domain.Entidades;

namespace TheosLivraria.Domain.Interface
{
    public interface IUsuarioRepository : IRepositoryBase<int, Usuario>
    {
        Task<Usuario?> ObterPorEmail(string email);
    }
}
