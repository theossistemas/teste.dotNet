using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JeffersonMello.Livraria.Data.Abstract
{
    public abstract class ConfigBase<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
    {

        #region Protected Fields

        #region Protected Fields

        protected EntityTypeBuilder<TEntity> builder;

        #endregion Protected Fields

        #endregion Protected Fields

        #region Protected Methods

        #region Protected Methods

        protected abstract void ConfigureFK();

        protected abstract void ConfigurePK();

        protected abstract void ConfigureFieldsTable();

        protected abstract void ConfigureTableName();

        #endregion Protected Methods

        #endregion Protected Methods

        #region Public Methods

        #region Public Methods

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            this.builder = builder;

            ConfigureTableName();
            ConfigureFieldsTable();
            ConfigurePK();
            ConfigureFK();
        }

        #endregion Public Methods

        #endregion Public Methods

    }
}