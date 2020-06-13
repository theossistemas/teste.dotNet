using Microsoft.EntityFrameworkCore;
using MMM.Library.Domain.Core.Data;
using MMM.Library.Domain.Core.Models;
using MMM.Library.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MMM.Library.Infra.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
     where TEntity : Entity, new()
    {
        protected readonly LibraryDbContext _dbContext;

        public RepositoryBase(LibraryDbContext context)
        {
            _dbContext = context;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(Guid id)
        {
             var entity = _dbContext.Set<TEntity>().Find(id);
             _dbContext.Set<TEntity>().Remove(entity);
        }

        // OPTIONS TO SOFT Delete ---
        public void DeleteSoft(Guid id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            entity.MarkAsDelete();
            Update(entity);
        }       

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(p => p.Id == id);
        }
        public virtual async Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }
        
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposedValue = false; // To detect redundant calls 
        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                _dbContext?.Dispose();
            }
            _disposedValue = true;
        }

    }
}
