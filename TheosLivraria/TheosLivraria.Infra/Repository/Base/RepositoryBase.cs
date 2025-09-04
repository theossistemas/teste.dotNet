using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Domain.Interface;
using TheosLivraria.Infra.Configuracao;

namespace TheosLivraria.Infra.Repository.Base
{
    public abstract class RepositoryBase<TId, TEntity> : IRepositoryBase<TId, TEntity>
        where TId : struct
        where TEntity : EntidadeBase<TId>
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> Alterar(TEntity entidade)
        {
            if(entidade == null)
                throw new ArgumentNullException(nameof(entidade));

            _dbSet.Update(entidade);
            return entidade;
        }

        public async Task<TEntity> Cadastrar(TEntity entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade));

            await _dbSet.AddAsync(entidade);
            return entidade;
        }

        public Task<bool> Delete(Guid uuid)
        {
            var entidade = _dbSet.FirstOrDefault(e => e.Uuid == uuid);
            if (entidade == null)
                return Task.FromResult(false);

            _dbSet.Remove(entidade);
            return Task.FromResult(true);
        }

        public async Task<bool> AlterarStatusRegistro(Guid uuid)
        {
            var entidade = await _dbSet.FirstOrDefaultAsync(e => e.Uuid == uuid);
            if (entidade == null)
                return false;

            if (entidade.Ativo)
                entidade.Desativar();
            else
                entidade.Ativar();

            _dbSet.Update(entidade);
            return true;

        }

        public async Task<bool> Reativar(Guid uuid)
        {
            var entidade = await _dbSet.FirstOrDefaultAsync(e => e.Uuid == uuid);
            if (entidade == null)
                return false;

            entidade.Ativar();
            _dbSet.Update(entidade);
            return true;

        }

        public async Task<TEntity?> ObterPorUuId(Guid uuid)
        {
            var entidade = await _dbSet.FirstOrDefaultAsync(e => e.Uuid == uuid);

            return entidade;
        }

    }
}
