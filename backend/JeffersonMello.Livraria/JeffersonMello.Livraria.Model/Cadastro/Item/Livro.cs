using JeffersonMello.Livraria.Model.Abstract;
using System;

namespace JeffersonMello.Livraria.Model.Cadastro.Item
{
    public class Livro : EntityBase
    {
        #region Public Properties

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Autor { get; set; }

        public string Marca { get; set; }

        public DateTime DataLancamento { get; set; }

        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }

        #endregion Public Properties
    }
}