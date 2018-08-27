using Livraria.Data.EFCore.Context;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Livraria.Data.EFCore.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : EntidadeBase
    {
        protected readonly LivrariaContext Context;
        protected readonly DbSet<T> Set;

        public Repository(LivrariaContext context)
        {
            Context = context;
            Set = Context.Set<T>();
        }

        public void Add(T obj)
        {
            obj.SetCreatedDateTime(DateTime.Now);
            Set.Add(obj);
        }

        public List<T> GetAll()
        {
            return Set.ToList();
        }

        public T GetById(Guid id)
        {
            return Set.Find(id);
        }

        public void Remove(Guid id)
        {
            Set.Remove(GetById(id));
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Update(T obj)
        {
            obj.SetUpdatedDateTime(DateTime.Now);
            Set.Update(obj);
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        public bool HasId(Guid id)
        {
            return Set.Any(x => x.Id == id);
        }
    }
}
