using JeffersonMello.Livraria.Model.Cadastro.Item;
using JeffersonMello.Livraria.Model.Filter.Cadastro.Item;
using JeffersonMello.Livraria.Repository.Abstract;
using JeffersonMello.Livraria.Strategy.Data.Cadastro.Item;
using Microsoft.EntityFrameworkCore;

namespace JeffersonMello.Livraria.Repository.Data.Cadastro.Item
{
    public class LivroRepository : RepositoryBase<Livro, LivroFilter, int>
    {
        #region Public Constructors

        public LivroRepository(DbContext context)
            : base(context)
        {
            strategy = LivroStrategy.GetInstance(context);
        }

        #endregion Public Constructors
    }
}