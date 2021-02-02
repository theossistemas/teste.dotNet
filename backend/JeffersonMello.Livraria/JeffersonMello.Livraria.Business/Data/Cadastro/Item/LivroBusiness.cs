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
    public class LivroBusiness : BusinessBase<Livro, LivroFilter, LivroRepository>, ILivroBusiness
    {
        #region Public Properties

        IGenericRepository<Livro, LivroFilter, int> IBusiness<Livro, LivroFilter>.Repository { get => base.Repository; set => base.Repository = value; }

        #endregion Public Properties

        #region Public Constructors

        public LivroBusiness(DbContext context)
                    : base(new LivroRepository(context))
        {
        }

        #endregion Public Constructors
    }
}