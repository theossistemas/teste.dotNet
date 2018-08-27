using Livraria.Domain.Entity;
using System.Collections.Generic;

namespace Livraria.Domain.Interface.QueryRepositories
{
    public interface IQueryRepository<TEntity> where TEntity : EntidadeBase
    {
        TEntity GetById(string id);
        IList<TEntity> GetList();
    }
}
