using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    [DataContract]
    public class AuthorBook : BaseEntity
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "person_id")]
        public int PersonId { get; set; }

        [DataMember(Name = "person")]
        public virtual Person Person { get; set; }

        [DataMember(Name = "book_id")]
        public int BookId { get; set; }

        [DataMember(Name = "book")]
        public virtual Book Book { get; set; }

        [DataMember(Name = "year_publication")]
        public int YearPublication { get; set; }

        public override object[] GetKey()
        {
            return new object[] { Id };
        }

    }
}
