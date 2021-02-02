using JeffersonMello.Livraria.Business.Abstract;
using JeffersonMello.Livraria.Business.Contract;
using JeffersonMello.Livraria.Business.Contract.Cadastro.Item;
using JeffersonMello.Livraria.Model.Cadastro.Item;
using JeffersonMello.Livraria.Model.Filter.Cadastro.Item;
using JeffersonMello.Livraria.Repository.Contract;
using JeffersonMello.Livraria.Repository.Data.Cadastro.Item;
using Microsoft.EntityFrameworkCore;

namespace JeffersonMello.Livraria.Business.Data.Cadastro.Item
{
    public class CategoriaBusiness : BusinessBase<Categoria, CategoriaFilter, CategoriaRepository>, ICategoriaBusiness
    {
        #region Public Properties

        IGenericRepository<Categoria, CategoriaFilter, int> IBusiness<Categoria, CategoriaFilter>.Repository { get => base.Repository; set => base.Repository = value; }

        #endregion Public Properties

        #region Public Constructors

        public CategoriaBusiness(DbContext context)
                    : base(new CategoriaRepository(context))
        {
        }

        #endregion Public Constructors
    }
}