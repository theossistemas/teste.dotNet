using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using TheosBookStore.LibCommon.Entities;

namespace TheosBookStore.LibCommon.Repositories
{
    public abstract class BaseRepository<TEntity, TDTO> : IBaseRepository<TEntity>
        where TEntity : Entity
        where TDTO : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TDTO> DbSet;
        protected readonly IMapper _mapper;

        protected BaseRepository(DbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            DbSet = _context.Set<TDTO>();
            _mapper = mapper;
        }

        public void Register(TEntity entity)
        {
            var data = _mapper.Map<TDTO>(entity);
            data = BeforePost(data, EntityState.Added);
            DbSet.Add(data);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            var data = _mapper.Map<TDTO>(entity);
            _context.Entry(data).State = EntityState.Modified;
            data = BeforePost(data, EntityState.Modified);
            DbSet.Update(data);
            _context.SaveChanges();
            _context.Entry(data).State = EntityState.Detached;
        }

        public void Remove(TEntity entity)
        {
            var data = _mapper.Map<TDTO>(entity);
            DbSet.Remove(data);
            _context.SaveChanges();
        }

        protected virtual TDTO BeforePost(TDTO model, EntityState state)
        {
            return model;
        }

        public TEntity GetById(int id)
        {
            var data = DbSet.Find(id);
            if (data == null)
                return null;
            _context.Entry(data).State = EntityState.Detached;
            var entity = _mapper.Map<TEntity>(data);
            return entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking().Select(dto => _mapper.Map<TEntity>(dto)).ToList();
        }

        protected void Unchange<T>(T model)
        {
            if (model == null)
                return;
            _context.Entry(model).State = EntityState.Unchanged;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                _context.Dispose();

                disposedValue = true;
            }
        }

        ~BaseRepository()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
