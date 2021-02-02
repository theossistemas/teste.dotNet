using System;

namespace JeffersonMello.Livraria.Model.Filter.Abstract
{
    public abstract class FilterBase
    {
        #region Public Properties

        public virtual int? Id { get; set; }

        public virtual DateTime? DataCriacao { get; set; }

        public virtual DateTime? DataAlteracao { get; set; }

        #endregion Public Properties
    }
}