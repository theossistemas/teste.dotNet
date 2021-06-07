using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected DataContext DataContext { get; set;}
        public Repository(DataContext dataContext)
        {
            DataContext = dataContext;
        }
        public IQueryable<T> GetAll()
        {
            return DataContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return DataContext.Set<T>().Where(expression).AsNoTracking();
        }
        public T Find(object[] keyValues)
        {
            return DataContext.Set<T>().Find(keyValues);
        }
        public void Insert(T entity)
        {
            DataContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            DataContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            DataContext.Set<T>().Remove(entity);
        }
    }
}
