using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Teste.App.viewModel
{
    [DataContract]
    public class BookViewModel
    {

        [DataMember(Name = "id")]
        public int? Id{ get; set; }

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

        [DataMember(Name = "personId")]
        public int PersonId { get; set; }

        [DataMember(Name = "yearPublication")]
        public int YearPublication { get; set; }

        [DataMember(Name = "authorBookId")]
        public int AuthorBookId { get; set; }
        //public virtual ICollection<AuthorBook> AuthorBooks { get; set; }


    }
}
