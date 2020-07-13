using LibraryStore.Core.Data.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryStore.Core.DataStorage
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        DatabaseFacade Database();
        Task<IList<TEntity>> FindAllAsync();
        IList<TEntity> FindAll();
        Task<TEntity> FindAsync(Guid guid);
        TEntity Find(Guid guid);
        TEntity Create(TEntity obj);
        Task<TEntity> CreateAsync(TEntity obj);
        bool Update(TEntity obj);
        Task<bool> UpdateAsync(TEntity obj);
        bool Remove(TEntity obj);
        Task<bool> RemoveAsync(TEntity obj);
        Task<bool> ExistsAsync(Guid guid);
        Task<bool> ExistsAsync(TEntity obj);
        bool Exists(Guid guid);
    }
}