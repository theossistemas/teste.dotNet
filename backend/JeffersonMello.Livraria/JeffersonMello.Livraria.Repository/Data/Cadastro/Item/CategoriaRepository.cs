using JeffersonMello.Livraria.Model.Cadastro.Item;
using JeffersonMello.Livraria.Model.Filter.Cadastro.Item;
using JeffersonMello.Livraria.Repository.Abstract;
using JeffersonMello.Livraria.Strategy.Data.Cadastro.Item;
using Microsoft.EntityFrameworkCore;

namespace JeffersonMello.Livraria.Repository.Data.Cadastro.Item
{
    public class CategoriaRepository : RepositoryBase<Categoria, CategoriaFilter, int>
    {
        #region Public Constructors

        public CategoriaRepository(DbContext context)
            : base(context)
        {
            strategy = CategoriaStrategy.GetInstance(context);
        }

        #endregion Public Constructors
    }
}