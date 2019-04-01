using System;
using System.Linq;

namespace ProjetoLivraria.Domain.Repositories.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity obj);
        TEntity GetById(Guid id);
        IQueryable<TEntity> GetAll();
        TEntity Update(TEntity obj);
        int Remove(Guid id);
        int SaveChanges();
    }
}
