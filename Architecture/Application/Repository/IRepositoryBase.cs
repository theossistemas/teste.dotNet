using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    public interface IRepositoryBase<TDomain> where TDomain : BaseDomain
    {
        Task<TDomain> CreateAsync(TDomain domain);
        Task<TDomain> CreateAsync(TDomain domain, Expression<Func<TDomain, bool>> conditionalToNotPersist);
        Task DeleteAsync(TDomain domain);
        Task<TDomain> DeleteByIdAsync(Guid id);
        Task<TDomain> DeleteByIdAsync(Guid id, Expression<Func<TDomain, bool>> conditionalToNotDelete);
        IQueryable<TDomain> GetAll(bool asNoTracking = true);
        Task<TDomain> GetByAsync(Expression<Func<TDomain, bool>> match, bool asNoTracking = true);
        Task<ICollection<TDomain>> GetAllByAsync(Expression<Func<TDomain, bool>> match, bool asNoTracking = true);
        Task<ICollection<TDomain>> GetAllByAsync(params Expression<Func<TDomain, bool>>[] match);
        Task<ICollection<TDomain>> GetAllByAsync(Expression<Func<TDomain, object>>[] includeProperties, Expression<Func<TDomain, bool>>[] match);
        Task<ICollection<TDomain>> GetAllAsync(bool asNoTracking = true);
        IQueryable<TDomain> GetAllIncluding(params Expression<Func<TDomain, object>>[] includeProperties);
        Task<TDomain> GetByIdAsync(Guid id);
        IQueryable<TDomain> GetByIncluding(Expression<Func<TDomain, bool>> match, bool asNoTracking = true,
            params Expression<Func<TDomain, object>>[] includeProperties);
        Task<ICollection<TDomain>> GetByIncludingAsync(Expression<Func<TDomain, bool>> match, bool asNoTracking = true,
            params Expression<Func<TDomain, object>>[] includeProperties);
        Task<TDomain> UpdateAsync(Guid id, TDomain domain);
        Task SaveChangesAsync();
        void Dispose();
    }
}
