using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Persistence.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        void Add(T item);
        T Edit(T item, T t);
        IEnumerable<T> Get();
        T GetById(int id);
        T GetByField(Expression<Func<T, bool>> expression);
        bool Save();
        void Remove(T item);
    }
}
