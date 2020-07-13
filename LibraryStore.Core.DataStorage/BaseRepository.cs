using LibraryStore.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryStore.Core.DataStorage
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly IDbContext context;

        protected readonly DbSet<TEntity> dbSet;

        protected BaseRepository(IDbContext context, DbSet<TEntity> dbSet)
        {
            this.context = context;
            this.dbSet = dbSet;
        }

        public DatabaseFacade Database()
            => context.Database;

        public TEntity Create(TEntity obj)
        {
            ApplyAlterContext(dbSet => dbSet.Add(obj));

            return obj;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity obj)
        {
            await ApplyAlterContextAsync(dbSet => dbSet.Add(obj));

            return obj;
        }

        public virtual bool Update(TEntity obj)
            => ApplyAlterContext(dbSet => dbSet.Update(obj));

        public virtual async Task<bool> UpdateAsync(TEntity obj)
            => await ApplyAlterContextAsync(dbSet => dbSet.Update(obj));

        public virtual bool Remove(TEntity obj)
            => ApplyAlterContext(dbSet => dbSet.Remove(obj));

        public virtual async Task<bool> RemoveAsync(TEntity obj)
            => await ApplyAlterContextAsync(dbSet => dbSet.Remove(obj));

        public virtual async Task<IList<TEntity>> FindAllAsync()
            => await dbSet.ToListAsync();

        public virtual IList<TEntity> FindAll()
            => dbSet.ToList();

        public virtual async Task<TEntity> FindAsync(Guid id)
            => await dbSet.AsNoTracking().FirstOrDefaultAsync(itm => itm.Id.Equals(id));

        public virtual TEntity Find(Guid id)
            => dbSet.AsNoTracking().FirstOrDefault(itm => itm.Id.Equals(id));

        public async Task<bool> ExistsAsync(Guid Id)
            => await dbSet.AnyAsync(itm => itm.Id.Equals(Id));

        public async Task<bool> ExistsAsync(TEntity obj)
            => await dbSet.AnyAsync(itm => itm.Id.Equals(obj.Id));

        public bool Exists(Guid Id)
            => dbSet.Any(itm => itm.Id.Equals(Id));

        private bool ApplyAlterContext(Action<DbSet<TEntity>> action)
        {
            action(dbSet);

            return context.SaveChanges() > 0;
        }

        private async Task<bool> ApplyAlterContextAsync(Action<DbSet<TEntity>> action)
        {
            action(dbSet);

            return await context.SaveChangesAsync() > 0;
        }
    }
}