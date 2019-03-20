using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teste.Impl.Context;

namespace Teste.Impl.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(DataContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public void set()
        {
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT SALE ON");
        }

        public IEnumerable<T> GetAll(string include)
        {
            if (string.IsNullOrWhiteSpace(include))
            {
                return entities.AsEnumerable();
            }

            return entities.Include(include).AsEnumerable();
        }

        public T Get(int id)
        {
            return entities.SingleOrDefault(s => Convert.ToInt32(s.GetKey().GetValue(0)) == id);
        }
        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }


        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
