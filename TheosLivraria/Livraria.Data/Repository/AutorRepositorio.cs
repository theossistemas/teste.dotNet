using Livraria.Data.Context;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Repository;

namespace Livraria.Data.Repository
{
    public class AutorRepositorio : Repository<Autor>, IAutorRepositorio
    {
        private readonly LivrariaContext _livrariaContext;

        public AutorRepositorio(LivrariaContext livrariaContext) : base(livrariaContext)
        {
            _livrariaContext = livrariaContext;
        }
    }
}
