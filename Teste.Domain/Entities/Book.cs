using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    [DataContract]
    public class Book : BaseEntity
    {
        public Book()
        {
            AuthorBooks = new HashSet<AuthorBook>();
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "publishingCompany")]
        public string PublishingCompany { get; set; }

        [DataMember(Name = "edition")]
        public int Edition { get; set; }

        [DataMember(Name = "pages")]
        public int Pages { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }

        public override object[] GetKey()
        {
            return new object[] { Id };
        }

    }
}
