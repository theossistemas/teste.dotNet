using JeffersonMello.Livraria.Model.Cadastro.Item;
using JeffersonMello.Livraria.Strategy.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;

namespace JeffersonMello.Livraria.Strategy.Data.Cadastro.Item
{
    public class CategoriaStrategy : StrategyBase<Categoria>
    {
        #region Private Fields

        private static Lazy<CategoriaStrategy> _instance;

        #endregion Private Fields

        #region Public Constructors

        public CategoriaStrategy(DbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public static CategoriaStrategy GetInstance(DbContext dbContext)
        {
            if (_instance == null)
                _instance = new Lazy<CategoriaStrategy>(() => new CategoriaStrategy(dbContext));

            return _instance.Value;
        }

        #endregion Public Methods
    }
}