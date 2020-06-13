using MMM.Library.Domain.Core.Models;
using System.Collections.Generic;

namespace MMM.Library.Domain.Models
{
    public class Category : Entity
    {
        public Category(int code, string categoryName)
        {
            Code = code;
            CategoryName = categoryName;
        }

        public int Code { get; private set; }
        public string CategoryName { get; private set; }

        // EF Relation
        public IEnumerable<Book> Books { get; set; }

        // Empty constructor for EF
        public Category() { }
    }
}
