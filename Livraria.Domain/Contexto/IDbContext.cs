namespace Livraria.Domain.Contexto
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Internal;

    using System;
    using System.Collections.Generic;

#pragma warning disable EF1001 // Internal EF Core API usage.
    public interface IDbContext : IDisposable, IInfrastructure<IServiceProvider>, IDbContextDependencies, IDbSetCache, IDbContextPoolable
    {
        DatabaseFacade Database { get; }
        ChangeTracker ChangeTracker { get; }
        EntityEntry Add(object entity);
        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        void AddRange(IEnumerable<object> entities);
        void AddRange(params object[] entities);
        EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Attach(object entity);
        void AttachRange(params object[] entities);
        void AttachRange(IEnumerable<object> entities);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Entry(object entity);
        bool Equals(object obj);
        object Find(Type entityType, params object[] keyValues);
        TEntity Find<TEntity>(params object[] keyValues) where TEntity : class;
        int GetHashCode();
        EntityEntry Remove(object entity);
        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
        void RemoveRange(IEnumerable<object> entities);
        void RemoveRange(params object[] entities);
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();
        EntityEntry Update(object entity);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        void UpdateRange(params object[] entities);
        void UpdateRange(IEnumerable<object> entities);
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
}