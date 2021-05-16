using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BookDTO
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}
