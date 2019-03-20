using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    [DataContract]
    public class Person : BaseEntity
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "birthDate")]
        public DateTime BirthDate { get; set; }

        [DataMember(Name = "cpf")]
        public string Cpf { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "genderId")]
        public int GenderId { get; set; }


        [DataMember(Name = "gender")]
        public virtual Gender Gender { get; set; }

        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }

        public override object[] GetKey()
        {
            return new object[] { Id };
        }
    }
}
