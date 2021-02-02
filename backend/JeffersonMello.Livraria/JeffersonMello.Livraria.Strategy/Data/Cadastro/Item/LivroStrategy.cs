using JeffersonMello.Livraria.Model.Cadastro.Item;
using JeffersonMello.Livraria.Strategy.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;

namespace JeffersonMello.Livraria.Strategy.Data.Cadastro.Item
{
    public class LivroStrategy : StrategyBase<Livro>
    {
        #region Private Fields

        private static Lazy<LivroStrategy> _instance;

        #endregion Private Fields

        #region Public Constructors

        public LivroStrategy(DbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public static LivroStrategy GetInstance(DbContext dbContext)
        {
            if (_instance == null)
                _instance = new Lazy<LivroStrategy>(() => new LivroStrategy(dbContext));
            return _instance.Value;
        }

        #endregion Public Methods
    }
}