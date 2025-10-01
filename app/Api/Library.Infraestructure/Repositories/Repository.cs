using System.Linq.Expressions;
using Library.Domain.Messages;
using Library.Domain.Interfaces;
using Library.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Library.Infraestructure.AppDbContext;

namespace Library.Infraestructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    public readonly DbSet<T> _dbSet;
    public readonly DataContext _dbContext;

    public Repository(DataContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
        _dbContext = dbContext;
    }

    public async Task<T> Insert(T entity, CancellationToken cancellation)
    {
        await _dbSet.AddAsync(entity, cancellation);
        await _dbContext.SaveChangesAsync(cancellation);
        return entity;
    }

    public async Task<T> GetById(Guid id, CancellationToken cancellation)
    {
        var entity = await _dbSet
            .AsQueryable()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellation);

        return entity!;
    }

    public async Task<List<T>> GetAll(
        Expression<Func<T, bool>> filter = null!,
        bool paginate = true,
        int page = 1,
        int pageSize = 10,
        string orderBy = "desc",
        CancellationToken? cancellation = null)
    {
        var query = _dbSet.AsQueryable();

        query = orderBy.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
            ? query.OrderBy(x => x.Id)
            : query.OrderByDescending(x => x.Id);

        if (page < 1) page = 1;
        if (paginate)
            query = query.Skip((page - 1) * pageSize)
                         .Take(pageSize);

        if (filter != null)
            query = query.Where(filter)
                         .AsNoTracking();

        return await query.ToListAsync(cancellation ?? CancellationToken.None);
    }

    public async Task<int> Count(
        Expression<Func<T, bool>> filter = null!,
        CancellationToken? cancellation = null)
    {
        var cancellationToken = cancellation ?? CancellationToken.None;
        if (filter != null)
            return await _dbSet.CountAsync(filter, cancellationToken);

        return await _dbSet.CountAsync(cancellationToken);
    }

    public async Task<T> FindOne(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellation)
    {
        var entity = await _dbSet.AsQueryable()
            .AsNoTracking()
            .AsQueryable()
            .FirstOrDefaultAsync(filter, cancellation);

        return entity!;
    }

    public async Task<T> Update(T entity, CancellationToken cancellation)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync(cancellation);
        return entity;
    }

    public async Task Delete(Guid id, CancellationToken cancellation)
    {
        var entity = await GetById(id, cancellation);

        if (entity is null)
            throw new ArgumentException(string.Format(EntityMessages.EMPTY, typeof(T).Name, $"id {id}"));

        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellation);
    }
}