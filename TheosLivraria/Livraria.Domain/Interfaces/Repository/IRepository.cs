using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Adicionar(TEntity obj);
        Task AdicionarAsync(TEntity obj);
        void Atualizar(TEntity obj);
        void Remover(TEntity obj);
        Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> ListarAsync();
        Task<TEntity> ObterPorIdAsync(int id);
    }
}
