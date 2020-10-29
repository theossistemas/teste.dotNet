using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Methods
        IQueryable<TEntity> FindAll { get; }
        TEntity Find(int key);
        void Insert(params TEntity[] obj);
        void Update(params TEntity[] obj);
        void Remove(params TEntity[] obj);
        #endregion

        #region Methods async  
        Task<IList<TEntity>> FindAllAsync();
        Task<TEntity> FindAsync(int key);
        Task InsertAsync(params TEntity[] obj);
        Task UpdateChangesAsync(TEntity obj);
        Task RemoveAsync(params TEntity[] obj);
        #endregion
    }
}
