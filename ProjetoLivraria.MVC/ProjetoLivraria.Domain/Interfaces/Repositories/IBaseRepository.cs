using System;
using System.Collections.Generic;

namespace ProjetoLivraria.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        TEntity FindById(int id);
        IEnumerable<TEntity> FindAll();
        void Update(TEntity obj);
        void Remove(TEntity obj);
        void Dispose();
    }
}
