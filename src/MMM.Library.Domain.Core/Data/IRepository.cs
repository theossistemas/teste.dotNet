using MMM.Library.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MMM.Library.Domain.Core.Data
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        void Add(TEntity entityToAdd);
        void Update(TEntity entityToUpdate);
        void Delete(Guid id);

        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        IUnitOfWork UnitOfWork { get; }
    }

   
}
