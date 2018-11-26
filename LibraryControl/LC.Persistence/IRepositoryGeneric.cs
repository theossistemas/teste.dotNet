using LC.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LC.Persistence
{
    public interface IRepositoryGeneric<TEntity>
        where TEntity : BaseEntity
    {
        TEntity Get(object[] key);
        Task<IEnumerable<TEntity>> GetAsync();
        IEnumerable<TEntity> Get(int pageSize, int page);
        TEntity Save(TEntity entity);
        bool Remove(TEntity entity);
        void Commit();
        void RollBack();
        void Begin();
        
    }
}
