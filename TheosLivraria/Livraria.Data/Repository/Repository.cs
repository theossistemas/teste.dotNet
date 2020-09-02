using Livraria.Data.Context;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Livraria.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        private readonly DbSet<TEntity> _dbSet;

        public Repository(LivrariaContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public async Task AdicionarAsync(TEntity obj) => await _dbSet.AddAsync(obj);
        public void Adicionar(TEntity obj) => _dbSet.Add(obj);

        public void Atualizar(TEntity obj) => _dbSet.Update(obj);

        public void Remover(TEntity obj) => _dbSet.Remove(obj);

        public async Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> ListarAsync() => await _dbSet.ToListAsync();

        public async Task<TEntity> ObterPorIdAsync(int id) =>
            await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}
