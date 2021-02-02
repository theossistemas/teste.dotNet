using JeffersonMello.Livraria.Common.Response;
using JeffersonMello.Livraria.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JeffersonMello.Livraria.Business.Contract
{
    public interface IBusiness<TEntity, TFilter>
        where TEntity : class, new()
        where TFilter : class, new()
    {
        #region Public Properties

        IGenericRepository<TEntity, TFilter, int> Repository { get; set; }

        #endregion Public Properties

        #region Public Methods

        List<TEntity> Get();

        List<TEntity> Get(TFilter filter);

        List<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        PaginateResponse<TEntity> Paginate(TFilter filter, int current = 1, int rowCount = 10);

        TEntity Get(int id);

        TEntity Save(TEntity entity);

        TEntity Update(TEntity entity);

        bool Delete(int id);

        bool Delete(TEntity entity);

        #endregion Public Methods
    }
}