using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Teste.App.viewModel
{
    public class AuthorBookViewModel
    {
        [DataMember(Name = "person_id")]
        public int PersonId { get; set; }

        [DataMember(Name = "id")]
        public int? Id { get; set; }


        [DataMember(Name = "person")]
        public virtual PersonViewModel Person { get; set; }

        [DataMember(Name = "book_id")]
        public int BookId { get; set; }

        [DataMember(Name = "book")]
        public virtual BookViewModel Book { get; set; }

        [DataMember(Name = "year_publication")]
        public int YearPublication { get; set; }
    }
}
