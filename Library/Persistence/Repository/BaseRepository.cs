using Persistence.Context;
using Persistence.Entity.Base;
using Persistence.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        private DataContext _dataContext { get; set; }

        public BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(T item)
        {
            _dataContext.Set<T>().Add(item);
        }

        public T Edit(T item, T t)
        {
            try
            {
                t.Id = item.Id;
                _dataContext.Entry(item).CurrentValues.SetValues(t);
                _dataContext.SaveChanges();
                return item;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao editar:" + e.Message);
            }
        }

        public IEnumerable<T> Get()
        {
            return _dataContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _dataContext.Set<T>().FirstOrDefault(x => x.Id.Equals(id));
        }

        public T GetByField(Expression<Func<T, bool>> expression)
        {
            return _dataContext.Set<T>().FirstOrDefault(expression);
        }

        public bool Save()
        {
            try
            {
                return _dataContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao salvar:" + e.Message);
            }
        }

        public void Remove(T item)
        {
            try
            {
                _dataContext.Remove(item);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao remover:" + e.Message);
            }
        }
    }
}
