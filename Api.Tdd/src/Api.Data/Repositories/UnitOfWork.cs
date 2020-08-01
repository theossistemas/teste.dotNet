using Api.Data.Repositories;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Data.Context;
using Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public sealed class UnitOfWork<T> : IUnitOfWork<T>, IDisposable where T : BaseEntity
    {
        private readonly MyContext _context;
        public UnitOfWork(MyContext context, IRepository<T> repository)
        {
            _context = context;
            Repository = repository;
        }
        public IRepository<T> Repository { get; }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}

