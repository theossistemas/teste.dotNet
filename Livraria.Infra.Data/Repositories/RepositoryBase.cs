
using Livraria.Domain;
using Livraria.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Livraria.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly Context _Context;

        public RepositoryBase(Context context)
        {
            _Context = context;
        }
        public IQueryable<TEntity> All => _Context.Set<TEntity>().AsQueryable();
        void IRepositoryBase<TEntity> .Add(TEntity obj)
        {
            _Context.Set<TEntity>().Add(obj);
            _Context.SaveChanges();
        }


        IEnumerable<TEntity> IRepositoryBase<TEntity>.GetAll()
        {
            return _Context.Set<TEntity>().AsNoTracking().ToList();
        }

        TEntity IRepositoryBase<TEntity>.GetById(int id)
        {
            return _Context.Set<TEntity>().Find(id);

        }
        void IRepositoryBase<TEntity>.Remove(TEntity obj)
        {
            _Context.Set<TEntity>().Remove(obj);
            _Context.SaveChanges();
        }

        void IRepositoryBase<TEntity>.Update(TEntity obj)
        {
            _Context.Entry(obj).State = EntityState.Modified;
            _Context.SaveChanges();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
