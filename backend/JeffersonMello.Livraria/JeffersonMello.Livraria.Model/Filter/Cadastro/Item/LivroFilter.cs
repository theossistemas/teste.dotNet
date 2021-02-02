using JeffersonMello.Livraria.Model.Filter.Abstract;
using System;

namespace JeffersonMello.Livraria.Model.Filter.Cadastro.Item
{
    public sealed class LivroFilter : FilterBase
    {
        #region Public Properties

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Autor { get; set; }

        public string Marca { get; set; }

        public DateTime? DataLancamento { get; set; }

        public int? CategoriaId { get; set; }

        #endregion Public Properties
    }
}