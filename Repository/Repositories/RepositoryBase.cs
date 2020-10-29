using Microsoft.EntityFrameworkCore;
using Repository.Connection;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields
        private readonly ApplicationContext _context;
        #endregion

        #region Constructors
        public RepositoryBase(ApplicationContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public IQueryable<TEntity> FindAll => _context.Set<TEntity>().AsQueryable();

        public void Insert(params TEntity[] obj)
        {
            _context.Set<TEntity>().AddRange(obj);
            _context.SaveChanges();
        }

        public void Update(params TEntity[] obj)
        {
            _context.Set<TEntity>().UpdateRange(obj);
            _context.SaveChanges();
        }

        public void Remove(params TEntity[] obj)
        {
            _context.Set<TEntity>().RemoveRange(obj);
            _context.SaveChanges();
        }

        public TEntity Find(int key)
        {
            return _context.Find<TEntity>(key);
        }
        #endregion

        #region Methods Async
        public async Task<IList<TEntity>> FindAllAsync() => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> FindAsync(int key)
        {
            return await _context.FindAsync<TEntity>(key);
        }

        public async Task InsertAsync(params TEntity[] obj)
        {
            _context.Set<TEntity>().AddRange(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateChangesAsync(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Set<TEntity>().UpdateRange(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(params TEntity[] obj)
        {
            _context.Set<TEntity>().RemoveRange(obj);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
