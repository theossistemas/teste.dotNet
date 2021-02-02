using JeffersonMello.Livraria.Business.Contract.Cadastro.Item;
using JeffersonMello.Livraria.Model.Cadastro.Item;
using JeffersonMello.Livraria.Tests.Data.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Linq;

namespace JeffersonMello.Livraria.Tests.Data.Cadastro.Item
{
    [TestClass]
    public class LivroTest : CRUDTestBase<Livro>
    {
        #region Private Fields

        private ILivroBusiness _livroBusiness;
        private ICategoriaBusiness _categoriaBusiness;

        #endregion Private Fields

        #region Protected Methods

        protected override Livro Create()
        {
            return _livroBusiness.Save(new Livro
            {
                Titulo = "Livro de Teste",
                Autor = "TestMan",
                Marca = "TestBrand",
                CategoriaId = _categoriaBusiness.Get().FirstOrDefault()?.Id ?? 0,
                DataLancamento = DateTime.Now.AddDays(new Random().Next(99)),
                Descricao = Faker.Lorem.Paragraph(100).Substring(0, 100)
            });
        }

        protected override void Delete(Livro entity)
        {
            _livroBusiness.Delete(entity);
        }

        protected override Livro Read(int id)
        {
            return _livroBusiness.Get(id);
        }

        protected override Livro Update(Livro entity)
        {
            entity.Titulo = "Livro de Teste Alterado";
            return _livroBusiness.Update(entity);
        }

        #endregion Protected Methods

        #region Public Methods

        [TestMethod]
        public override void RunCRUD()
        {
            _livroBusiness = kernel.Get<ILivroBusiness>();
            _categoriaBusiness = kernel.Get<ICategoriaBusiness>();

            var entity = Create();
            entity = Update(entity);
            var entityRead = Read(entity.Id);
            Delete(entityRead);
        }

        #endregion Public Methods
    }
}