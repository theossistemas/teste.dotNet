using JeffersonMello.Livraria.Common.Response;
using JeffersonMello.Livraria.Model.Abstract;
using JeffersonMello.Livraria.Model.Filter.Abstract;
using JeffersonMello.Livraria.Repository.Abstract;
using JeffersonMello.Livraria.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JeffersonMello.Livraria.Business.Abstract
{
    public abstract class BusinessBase<TEntity, TFilter, TRepository>
        where TEntity : EntityBase, new()
        where TRepository : RepositoryBase<TEntity, TFilter, int>
        where TFilter : FilterBase, new()
    {
        #region Protected Properties

        protected IGenericRepository<TEntity, TFilter, int> Repository { get; set; }

        #endregion Protected Properties

        #region Public Constructors

        public BusinessBase(RepositoryBase<TEntity, TFilter, int> repository)
        {
            Repository = repository;
        }

        public BusinessBase(IGenericRepository<TEntity, TFilter, int> repository)
        {
            Repository = repository;
        }

        #endregion Public Constructors

        #region Public Methods

        public List<TEntity> Get()
        {
            try
            {
                var entities = Repository.Select();
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TEntity> Get(TFilter filter)
        {
            try
            {
                var entities = Repository.ApplyFilter(filter);
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entities = Repository.Select(predicate);
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity Get(int id)
        {
            try
            {
                var entity = Repository.SelectById(id);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity Save(TEntity entity)
        {
            try
            {
                entity = Repository.Save(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity Update(TEntity entity)
        {
            try
            {
                entity = Repository.Update(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Repository.DeleteById(id);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                Repository.Delete(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PaginateResponse<TEntity> Paginate(TFilter filter, int current = 1, int rowCount = 10)
        {
            try
            {
                var result = Repository.Paginate(filter, current, rowCount);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public Methods
    }
}