using JeffersonMello.Livraria.Common.Extension;
using JeffersonMello.Livraria.Common.Response;
using JeffersonMello.Livraria.Model.Abstract;
using JeffersonMello.Livraria.Model.Filter.Abstract;
using JeffersonMello.Livraria.Repository.Contract;
using JeffersonMello.Livraria.Strategy.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace JeffersonMello.Livraria.Repository.Abstract
{
    public abstract class RepositoryBase<TEntity, TFilter, TKey> : IGenericRepository<TEntity, TFilter, TKey>
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
    {
        #region Protected Fields

        protected StrategyBase<TEntity> strategy;

        protected DbContext _context;

        #endregion Protected Fields

        #region Public Constructors

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual void AlterContext(DbContext context)
        {
            _context = context;
            strategy.AlterContext(context);
        }

        public DbContext GetContext()
        {
            return _context;
        }

        public virtual void Delete(TEntity entity)
        {
            if (strategy != null)
            {
                strategy.BeforeDelete(entity);
            }

            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void DeleteById(TKey id)
        {
            TEntity entity = SelectById(id);
            Delete(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            entity.DataAlteracao = DateTime.Now;

            if (strategy != null)
            {
                strategy.Validate(entity, true);
                strategy.BeforeSave(entity, true);
            }

            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            if (strategy != null)
                strategy.AfterSave(entity, true);

            return entity;
        }

        public virtual TEntity Save(TEntity entity)
        {
            if (strategy != null)
            {
                strategy.Validate(entity, false);
                strategy.BeforeSave(entity, false);
            }

            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

            if (strategy != null)
                strategy.AfterSave(entity, false);

            return entity;
        }

        public virtual List<TEntity> Select()
        {
            return _context.Set<TEntity>().ToList();
        }

        public virtual List<TEntity> Select(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToList();
        }

        public virtual PaginateResponse<TEntity> Paginate(TFilter filter, int current = 1, int rowCount = 10)
        {
            var entities = new List<TEntity>();
            var response = new PaginateResponse<TEntity>();

            try
            {
                entities = ApplyFilter(filter);
                int total = entities.Count;

                if (rowCount != -1)
                {
                    entities = entities.Skip((current - 1) * rowCount).Take(rowCount).ToList();
                }

                response.Current = current;
                response.RowCount = rowCount;
                response.Total = total;
                response.Rows = entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public virtual TEntity SelectById(TKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual List<TEntity> ApplyFilter(TFilter filter)
        {
            List<TEntity> entities = new List<TEntity>();

            string whereCommand = "";
            var filters = filter.GetType()
                                .GetProperties(
                                        BindingFlags.Public
                                        | BindingFlags.Instance);
            var paramnsCount = 0;
            List<object> values = new List<object>();

            try
            {
                foreach (var propertyInfo in filters)
                {
                    if (propertyInfo.IsValid())
                    {
                        var value = propertyInfo.GetValue(filter);

                        if (value != null)
                        {
                            values.Add(value);

                            if (value is string)
                            {
                                whereCommand += $"{propertyInfo.Name}.ToLower().Contains(@{paramnsCount}.ToLower()) ";
                            }
                            else if (value is DateTime)
                            {
                                whereCommand += $"{propertyInfo.Name} = @{paramnsCount} ";
                            }
                            else if (value is TimeSpan)
                            {
                                whereCommand += $"{propertyInfo.Name} = @{paramnsCount} ";
                            }
                            else
                            {
                                whereCommand += $"{propertyInfo.Name} = @{paramnsCount} ";
                            }

                            paramnsCount++;
                            whereCommand += "or ";
                        }
                    }
                }

                whereCommand = whereCommand.Substring(0, whereCommand.Length - 3);
                entities = Select().AsQueryable().Where(whereCommand, values.ToArray()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entities;
        }

        #endregion Public Methods
    }
}