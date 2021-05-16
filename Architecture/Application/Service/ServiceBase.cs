using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    public abstract class ServiceBase<TDomain>: IServiceBase<TDomain> where TDomain : BaseDomain
    {
        protected readonly IRepositoryBase<TDomain> _repository;

        public ServiceBase(IRepositoryBase<TDomain> repository)
        {
            this._repository = repository;            
        }

        public virtual async Task<TDomain> CreateAsync(TDomain obj)
        {
            var result = await this._repository.CreateAsync(obj);
            
            return result;
        }

        public virtual async Task<TDomain> DeleteAsync(Guid id)
        {
            var result = await this._repository.DeleteByIdAsync(id);
            
            return result;
        }

        public virtual async Task<TDomain> DeleteAsync(Guid id, Expression<Func<TDomain, bool>> conditionalToNotDelete)
        {
            var result = await this._repository.DeleteByIdAsync(id, conditionalToNotDelete);
            
            return result;
        }

        public virtual async Task<ICollection<TDomain>> GetAllByAsync(Expression<Func<TDomain, bool>> match, 
            bool asNoTracking = true)
        {
            return await this._repository.GetAllByAsync(match, asNoTracking);
        }

        public virtual async Task<ICollection<TDomain>> GetAllByAsync(params Expression<Func<TDomain, bool>>[] match)
        {
            return await this._repository.GetAllByAsync(match);
        }

        public virtual async Task<ICollection<TDomain>> GetAllByAsync(Expression<Func<TDomain, object>>[] includeProperties, Expression<Func<TDomain, bool>>[] match)
        {
            return await this._repository.GetAllByAsync(includeProperties, match);
        }

        public virtual async Task<ICollection<TDomain>> GetAllAsync()
        {
            return await this._repository.GetAllAsync();
        }
        public virtual IQueryable<TDomain> GetAllIncluding(params Expression<Func<TDomain, object>>[] includeProperties)
        {
            return this._repository.GetAllIncluding(includeProperties);
        }
        public virtual async Task<TDomain> GetByIdAsync(Guid id)
        {
            return await this._repository.GetByIdAsync(id);
        }

        public virtual async Task<ICollection<TDomain>> GetByIncludingAsync(Expression<Func<TDomain, bool>> match, bool asNoTracking = true,
            params Expression<Func<TDomain, object>>[] includeProperties)
        {
            return await this._repository.GetByIncludingAsync(match, asNoTracking, includeProperties);
        }

        public virtual async Task<TDomain> UpdateAsync(Guid id, TDomain obj)
        {
            await this._repository.UpdateAsync(id, obj);
            
            return obj;
        }

        public async Task SaveChangeAsync()
        {
            await this._repository.SaveChangesAsync();
        }

        public void Dispose()
        {
            this._repository.Dispose();
        }
    }
}
