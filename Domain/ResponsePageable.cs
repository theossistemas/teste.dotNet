using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ResponsePageable<TDomain> where TDomain: class
    {
        public ICollection<TDomain> Content { get; set; }
        public int TotalRecords { get; set; }
        public ICollection<string> Informations { get; set; }
    }
}
