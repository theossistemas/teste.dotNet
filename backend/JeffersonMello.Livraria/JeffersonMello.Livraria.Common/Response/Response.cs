namespace JeffersonMello.Livraria.Common.Response
{
    public class Response<T>
    {
        #region Public Properties

        public bool Success { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        #endregion Public Properties
    }

    public class Response
    {
        #region Public Properties

        public bool Success { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }

        #endregion Public Properties
    }
}