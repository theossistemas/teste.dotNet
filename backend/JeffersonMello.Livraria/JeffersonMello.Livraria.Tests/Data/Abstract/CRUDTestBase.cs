namespace JeffersonMello.Livraria.Tests.Data.Abstract
{
    public abstract class CRUDTestBase<TEntity> : DataTestBase
        where TEntity : class, new()
    {
        #region Protected Methods

        protected abstract TEntity Create();

        protected abstract TEntity Read(int id);

        protected abstract TEntity Update(TEntity entity);

        protected abstract void Delete(TEntity entity);

        public abstract void RunCRUD();

        #endregion Protected Methods
    }
}