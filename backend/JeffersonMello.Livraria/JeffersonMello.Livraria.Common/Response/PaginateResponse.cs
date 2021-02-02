using System.Collections.Generic;

namespace JeffersonMello.Livraria.Common.Response
{
    public class PaginateResponse<TEntity>
    {
        #region Public Properties

        public int Current { get; set; }

        public IList<TEntity> Rows { get; set; }

        public int Total { get; set; }

        public int RowCount { get; set; }

        #endregion Public Properties
    }
}