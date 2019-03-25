using System;
using System.Collections.Generic;

namespace Theos.Library.CrossCutting.Response
{
    public class ResponseModel<T> where T : class
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int LastPage => PerPage > 0 ? Convert.ToInt16(Math.Ceiling((double)Total / PerPage)) : 1;
        public int Total { get; set; }
    }
}
