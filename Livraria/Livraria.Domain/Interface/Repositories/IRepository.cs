using Livraria.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Livraria.Domain.Interface.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntidadeBase
    {
        void Add(TEntity obj);
        bool HasId(Guid id);
        TEntity GetById(Guid id);
        List<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(Guid id);
        int SaveChanges();
    }
}
