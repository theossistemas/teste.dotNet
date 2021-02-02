using System;

namespace JeffersonMello.Livraria.Model.Abstract
{
    public abstract class EntityBase
    {
        #region Protected Properties

        public virtual int Id { get; set; }

        public virtual DateTime DataCriacao { get; set; } = DateTime.Now;

        public virtual DateTime DataAlteracao { get; set; } = DateTime.Now;

        #endregion Protected Properties
    }
}