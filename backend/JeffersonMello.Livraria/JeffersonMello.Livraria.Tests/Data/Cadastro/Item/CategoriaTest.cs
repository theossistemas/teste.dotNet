using JeffersonMello.Livraria.Business.Contract.Cadastro.Item;
using JeffersonMello.Livraria.Model.Cadastro.Item;
using JeffersonMello.Livraria.Tests.Data.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace JeffersonMello.Livraria.Tests.Data.Cadastro.Item
{
    [TestClass]
    public class CategoriaTest : CRUDTestBase<Categoria>
    {
        #region Private Fields

        private ICategoriaBusiness _categoriaBusiness;

        #endregion Private Fields

        #region Protected Methods

        protected override Categoria Create()
        {
            return _categoriaBusiness.Save(new Categoria
            {
                Descricao = "Nova Categoria"
            });
        }

        protected override void Delete(Categoria entity)
        {
            _categoriaBusiness.Delete(entity);
        }

        protected override Categoria Read(int id)
        {
            return _categoriaBusiness.Get(id);
        }

        protected override Categoria Update(Categoria entity)
        {
            entity.Descricao = "Nova Categoria Alterada";
            return _categoriaBusiness.Update(entity);
        }

        #endregion Protected Methods

        #region Public Methods

        [TestMethod]
        public override void RunCRUD()
        {
            _categoriaBusiness = kernel.Get<ICategoriaBusiness>();

            var entity = Create();
            entity = Update(entity);
            var entityRead = Read(entity.Id);
            Delete(entityRead);
        }

        #endregion Public Methods
    }
}