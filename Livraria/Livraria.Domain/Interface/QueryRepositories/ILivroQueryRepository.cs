using Livraria.Domain.Entity;
using System.Collections.Generic;

namespace Livraria.Domain.Interface.QueryRepositories
{
    public interface ILivroQueryRepository : IQueryRepository<Livro>
    {
        IList<Livro> ListarOrdenadoPorNome();
    }
}
