using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    [DataContract]
    public class Address : BaseEntity
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "street")]
        public string Street { get; set; }

        [DataMember(Name = "number")]
        public int Number { get; set; }

        [DataMember(Name = "neighborhood")]
        public string Neighborhood { get; set; }

        [DataMember(Name = "cep")]
        public string Cep { get; set; }

        [DataMember(Name = "complement")]
        public string Complement { get; set; }

        [DataMember(Name = "person_id")]
        public int? PersonId { get; set; }

        [DataMember(Name = "city_id")]
        public int CityId { get; set; }

        [DataMember(Name = "city")]
        public virtual City City { get; set; }

        public override object[] GetKey()
        {
            return new object[] { Id };
        }
    }
}
