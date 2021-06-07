using System;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        T Find(object[] keyValues);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
