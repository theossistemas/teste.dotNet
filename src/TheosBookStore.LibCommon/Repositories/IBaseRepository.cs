using System;
using System.Collections.Generic;

using TheosBookStore.LibCommon.Entities;

namespace TheosBookStore.LibCommon.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        void Register(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
