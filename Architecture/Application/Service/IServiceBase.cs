using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    public interface IServiceBase<TDomain> where TDomain: BaseDomain
    {
        Task<TDomain> CreateAsync(TDomain obj);
        Task<TDomain> DeleteAsync(Guid id);
        Task<TDomain> DeleteAsync(Guid id, Expression<Func<TDomain, bool>> conditionalToNotDelete);
        Task<ICollection<TDomain>> GetAllByAsync(Expression<Func<TDomain, bool>> match,
            bool asNoTracking = true);
        Task<ICollection<TDomain>> GetAllByAsync(params Expression<Func<TDomain, bool>>[] match);
        Task<ICollection<TDomain>> GetAllByAsync(Expression<Func<TDomain, object>>[] includeProperties, Expression<Func<TDomain, bool>>[] match);
        Task<ICollection<TDomain>> GetAllAsync();
        IQueryable<TDomain> GetAllIncluding(params Expression<Func<TDomain, object>>[] includeProperties);
        Task<TDomain> GetByIdAsync(Guid id);
        Task<ICollection<TDomain>> GetByIncludingAsync(Expression<Func<TDomain, bool>> match, bool asNoTracking = true,
            params Expression<Func<TDomain, object>>[] includeProperties);
        Task<TDomain> UpdateAsync(Guid id, TDomain obj);
        Task SaveChangeAsync();
        void Dispose();
    }
}
