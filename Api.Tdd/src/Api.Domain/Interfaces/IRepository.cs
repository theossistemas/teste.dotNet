using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<IEnumerable<T>> InsertListAsync(IEnumerable<T> item);
        Task<IEnumerable<T>> UpdateListAsync(IEnumerable<T> item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteLogicAsync(int id);
        Task<bool> DeleteFisicoAsync(int id);
        Task<bool> DeleteList(IEnumerable<T> item);
        Task<T> SelectAsync(int id);
        Task<IEnumerable<T>> SelectAsync();
        Task<bool> ExistAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> SelectWhereAsync(Expression<Func<T, bool>> where);
        IQueryable<T> GetDataSet();
    }
}
