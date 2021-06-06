using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaWeb.Domain.Interface
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity Objeto);

        Task<int> Update(TEntity Objeto);

        Task Delete(TEntity Objeto);

        Task<TEntity> GetEntityById(long Id);

        Task<List<TEntity>> GetAll();
    }
}