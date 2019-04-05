using System;
using System.Collections.Generic;


namespace ProjetoLivraria.Application.Interface
{
    public interface IBaseApplication<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        TEntity FindGetById(int id);
        IEnumerable<TEntity> FindAll();
        void Update(TEntity obj);
        void Remove(TEntity obj);
        void Dispose();
    }
}
