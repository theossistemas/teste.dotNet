using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Domain
{
    public class RequestFilter
    {
        public int? Index { get; set; }
        public int? PageSize { get; set; }
        public OrderBy OrderBy { get; set; }
    }

    public class OrderBy
    {
        public string Field { get; set; }
        public bool Asc { get; set; }
    }
}
