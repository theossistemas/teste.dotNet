using Livraria.Common.Utils;
using Livraria.Data.Context;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace Livraria.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly ILogger _logger;

        public Repository(LivrariaContext context, ILogger logger)
        {
            _dbSet = context.Set<TEntity>();
            _logger = logger;
        }

        public async Task AdicionarAsync(TEntity obj)
        { //=> await _dbSet.AddAsync(obj); 
            await _dbSet.AddAsync(obj);
            Log(obj, Resources.AdicionadoLogger);

        }

        public void Adicionar(TEntity obj)
        {//=> _dbSet.Add(obj);
            _dbSet.Add(obj);
            Log(obj, Resources.AdicionadoLogger);
        }

        public void Atualizar(TEntity obj) //=> _dbSet.Update(obj);
        {
            _dbSet.Update(obj);
            Log(obj, Resources.AlteradoLogger);
        }

        public void Remover(TEntity obj) //=> _dbSet.Remove(obj);
        {
            _dbSet.Remove(obj);
            Log(obj, Resources.removidoLogger);
        }

        public async Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> ListarAsync() => await _dbSet.ToListAsync();

        public async Task<TEntity> ObterPorIdAsync(int id) =>
            await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));

        private void Log(TEntity obj, string acao)
        {
            var options = new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            };
            var json = JsonSerializer.Serialize<TEntity>(obj, options);
            _logger.LogInformation(string.Format(Resources.EntidadeLogger, json, acao));

        }
    }
}
