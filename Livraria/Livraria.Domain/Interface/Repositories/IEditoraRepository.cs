using Livraria.Domain.Entity;

namespace Livraria.Domain.Interface.Repositories
{
    public interface IEditoraRepository : IRepository<Editora>
    {
        bool IsNomeRegistered(string nome);
        Editora BuscarPorNome(string nome);
    }
}
