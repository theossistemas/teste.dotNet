using Livraria.Domain.Entity;

namespace Livraria.Domain.Interface.Repositories
{
    public interface IAutorRepository : IRepository<Autor>
    {
        bool IsNomeRegistered(string nome);
        Autor BuscarPorNome(string nome);
    }
}
