using Domain;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    public abstract class RepositoryBase<TDomain, TContext> : IRepositoryBase<TDomain>
        where TDomain : BaseDomain where TContext : DbContext
    {
        protected readonly TContext _dbContext;
        protected readonly DbSet<TDomain> _entitySet;
        private bool disposed = false;

        public RepositoryBase(TContext dbContext)
        {
            this._dbContext = dbContext;
            this._entitySet = this._dbContext.Set<TDomain>();
        }

        public virtual async Task<TDomain> CreateAsync(TDomain domain)
        {
            await this._entitySet.AddAsync(domain);
            
            return domain;
        }

        public virtual async Task<TDomain> CreateAsync(TDomain domain, Expression<Func<TDomain, bool>> conditionalToNotPersist)
        {

            if (await this._entitySet.AnyAsync(conditionalToNotPersist))
            {
                throw new ValidationException("Data exist.");
            }
            await this._entitySet.AddAsync(domain);

            return domain;
        }

        public virtual Task DeleteAsync(TDomain domain)
        {
            this._entitySet.Remove(domain).State = EntityState.Deleted;
            return Task.CompletedTask;
        }

        public virtual async Task<TDomain> DeleteByIdAsync(Guid id)
        {
            TDomain entity = await this._entitySet.FindAsync(id);

            if (entity != null)
            {
                this._entitySet.Remove(entity).State = EntityState.Deleted;
            }
            return entity;
        }

        public virtual async Task<TDomain> DeleteByIdAsync(Guid id, Expression<Func<TDomain, bool>> conditionalToNotDelete)
        {
            if (this._entitySet.Any(conditionalToNotDelete))
            {
                throw new ValidationException("Exist a conditional for not delete.");
            }
            var entity = await this.DeleteByIdAsync(id);

            return entity;
        }

        public virtual IQueryable<TDomain> GetAll(bool asNoTracking = true)
        {
            if (asNoTracking)
                return this._entitySet.AsNoTracking();

            return this._entitySet;
        }

        public virtual async Task<TDomain> GetByAsync(Expression<Func<TDomain, bool>> match, bool asNoTracking = true)
        {
            TDomain response = null;
            if (asNoTracking)
            {
                response = await this._entitySet.AsNoTracking().FirstOrDefaultAsync(match);
                return response;
            }

            response = await this._entitySet.FirstOrDefaultAsync(match);
            return response;
        }

        public virtual async Task<ICollection<TDomain>> GetAllByAsync(Expression<Func<TDomain, bool>> match,
            bool asNoTracking = true)
        {
            if (asNoTracking)
            {
                return await this._entitySet.AsNoTracking().Where(match).ToListAsync();
            }
            return await this._entitySet.Where(match).ToListAsync();
        }

        public virtual async Task<ICollection<TDomain>> GetAllByAsync(params Expression<Func<TDomain, bool>>[] match)
        {
            var query = this.GetAll();

            foreach(var expression in match)
            {
                query = query.Where(expression);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<ICollection<TDomain>> GetAllByAsync(Expression<Func<TDomain, object>>[] includeProperties, Expression<Func<TDomain, bool>>[] match)
        {
            IQueryable<TDomain> queryable = GetAll();
            foreach (Expression<Func<TDomain, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<TDomain, object>(includeProperty);
            }

            foreach (var expression in match)
            {
                queryable = queryable.Where(expression);
            }

            return await queryable.ToListAsync();
        }

        public virtual async Task<ICollection<TDomain>> GetAllAsync(bool asNoTracking = true)
        {
            var result = this._entitySet.AsQueryable<TDomain>();

            if (asNoTracking)
            {
                result = this._entitySet.AsNoTracking();
            }

            return await result.ToListAsync();
        }

        public virtual IQueryable<TDomain> GetAllIncluding(params Expression<Func<TDomain, object>>[] includeProperties)
        {
            IQueryable<TDomain> queryable = GetAll(true);
            foreach (Expression<Func<TDomain, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<TDomain, object>(includeProperty);
            }
            return queryable;
        }

        public virtual async Task<TDomain> GetByIdAsync(Guid id)
        {
            var result = await this._dbContext.FindAsync<TDomain>(id);
            if (result == null)
            {
                throw new KeyNotFoundException("Cliente Not Found");
            }
            return result;
        }

        public virtual IQueryable<TDomain> GetByIncluding(Expression<Func<TDomain, bool>> match, bool asNoTracking = true,
            params Expression<Func<TDomain, object>>[] includeProperties)
        {
            IQueryable<TDomain> queryable = GetAll(asNoTracking);
            foreach (Expression<Func<TDomain, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<TDomain, object>(includeProperty);
            }

            return queryable.Where(match);
        }

        public virtual async Task<ICollection<TDomain>> GetByIncludingAsync(Expression<Func<TDomain, bool>> match, bool asNoTracking = true,
            params Expression<Func<TDomain, object>>[] includeProperties)
        {
            var result = await GetByIncluding(match, asNoTracking, includeProperties).ToListAsync();

            return result;
        }

        public virtual async Task<TDomain> UpdateAsync(Guid id, TDomain domain)
        {
            var entity = await this.GetByIdAsync(id);

            this._dbContext.Entry(entity).CurrentValues.SetValues(domain);
            this._entitySet.Update(entity).State = EntityState.Modified;

            return entity;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public virtual async Task SaveChangesAsync()
        {
            await this._dbContext.SaveChangesAsync();
            this.Dispose();
        }
    }
}
