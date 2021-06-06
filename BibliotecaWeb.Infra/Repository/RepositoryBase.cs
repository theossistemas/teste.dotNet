using LivrariaWeb.Domain.Interface;
using Microsoft.Win32.SafeHandles;
using LivrariaWeb.Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace LivrariaWeb.Infra.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IDisposable where TEntity : class
    {
        protected readonly DbContextOptions<ContextBdLivraria> _contextBdLivraria;
        protected DbSet<TEntity> _dbSet;
        protected ContextBdLivraria _dataContext;
        public RepositoryBase(DbContextOptions<ContextBdLivraria> contextBdLivraria)
        {
            _contextBdLivraria = contextBdLivraria;
            _dataContext = new ContextBdLivraria(_contextBdLivraria);
            _dbSet = _dataContext.Set<TEntity>();
        }

        public void Add(TEntity Objeto)
        {
            _dbSet.Add(Objeto);
            _dataContext.SaveChanges();
        }

        public async Task Delete(TEntity Objeto)
        {
            using (var data = new ContextBdLivraria(_contextBdLivraria))
            {
                data.Set<TEntity>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();

            }

            disposed = true;
        }

        public async Task<TEntity> GetEntityById(long Id)
        {
            using (var data = new ContextBdLivraria(_contextBdLivraria))
            {
                return await data.Set<TEntity>().FindAsync(Id);
            }
        }

        public async Task<List<TEntity>> GetAll()
        {
            using (var data = new ContextBdLivraria(_contextBdLivraria))
            {
                return await data.Set<TEntity>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<int> Update(TEntity Objeto)
        {
            using (var data = new ContextBdLivraria(_contextBdLivraria))
            {
                var update = data.Set<TEntity>().Update(Objeto);
                return await data.SaveChangesAsync();
            }
        }
    }
}
