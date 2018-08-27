using Livraria.Data.EFCore.Context;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface.Repositories;
using System.Linq;

namespace Livraria.Data.EFCore.Repositories
{
    public class EditoraRepository : Repository<Editora>, IEditoraRepository
    {
        public EditoraRepository(LivrariaContext context) : base(context)
        {
        }

        public Editora BuscarPorNome(string nome)
        {
            return Set.Where(x => x.Nome.ToLower() == nome.ToLower()).FirstOrDefault();
        }

        public bool IsNomeRegistered(string nome)
        {
            return Set.Any(x => x.Nome.ToLower() == nome.ToLower());
        }
    }
}
