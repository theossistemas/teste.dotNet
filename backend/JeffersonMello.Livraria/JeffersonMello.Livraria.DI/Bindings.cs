using JeffersonMello.Livraria.Business.Contract.Cadastro.Item;
using JeffersonMello.Livraria.Business.Data.Cadastro.Item;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Ninject.Modules;
using System;

namespace JeffersonMello.Livraria.DI
{
    public class Bindings : NinjectModule
    {
        #region Private Fields

        private DbContext _context;

        private StandardKernel _kernel;

        #endregion Private Fields

        #region Public Constructors

        public Bindings(StandardKernel kernel, DbContext dataContext)
        {
            this._context = dataContext;
            this._kernel = kernel;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            try
            {
                _kernel.Bind<ICategoriaBusiness>()
                            .To<CategoriaBusiness>()
                            .WithConstructorArgument("context", _context);

                _kernel.Bind<ILivroBusiness>()
                            .To<LivroBusiness>()
                            .WithConstructorArgument("context", _context);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public Methods
    }
}