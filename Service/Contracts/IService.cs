using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IService<TEntity> where TEntity : class
    {
        #region Methods async  
        Task<IList<TEntity>> FindAllAsync();
        Task<TEntity> FindAsync(int key);
        Task InsertAsync(TEntity obj);
        Task UpdateChangesAsync(TEntity obj);
        Task RemoveAsync(int key);
        #endregion
    }
}
