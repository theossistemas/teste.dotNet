using Microsoft.EntityFrameworkCore;

namespace JeffersonMello.Livraria.Strategy.Data.Abstract
{
    public abstract class StrategyBase<TEntity>
       where TEntity : class, new()
    {
        #region Protected Fields

        protected DbContext DbContext;

        #endregion Protected Fields

        #region Public Constructors

        public StrategyBase(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual void AlterContext(DbContext context)
        {
            DbContext = context;
        }

        public virtual TEntity PrepareStrategy(TEntity entity, bool updating)
        {
            return entity;
        }

        public virtual TEntity Validate(TEntity entity, bool updating)
        {
            return entity;
        }

        public virtual TEntity BeforeSave(TEntity entity, bool updating)
        {
            return entity;
        }

        public virtual TEntity AfterSave(TEntity entity, bool updating)
        {
            return entity;
        }

        public virtual TEntity BeforeDelete(TEntity entity)
        {
            return entity;
        }

        public virtual TEntity AfterDelete(TEntity entity)
        {
            return entity;
        }

        #endregion Public Methods
    }
}