using JeffersonMello.Livraria.Common.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JeffersonMello.Livraria.Repository.Contract
{
    public interface IGenericRepository<TEntity, TFilter, TKey>
    {
        #region Public Methods

        List<TEntity> Select();        

        List<TEntity> Select(Expression<Func<TEntity, bool>> predicate);

        TEntity SelectById(TKey id);

        TEntity Save(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteById(TKey id);

        PaginateResponse<TEntity> Paginate(TFilter filter, int current = 1, int rowCount = 10);

        List<TEntity> ApplyFilter(TFilter filter);

        void AlterContext(DbContext context);

        DbContext GetContext();

        #endregion Public Methods
    }
}