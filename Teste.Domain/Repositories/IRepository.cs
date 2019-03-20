using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll(string include);
        T Get(int id);
        T Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
