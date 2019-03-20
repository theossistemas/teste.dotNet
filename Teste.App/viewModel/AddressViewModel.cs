using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Teste.App.viewModel
{
    [DataContract]
    public class AddressViewModel
    {
        [DataMember(Name = "id")]
        public int? Id { get; set; }

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

        [DataMember(Name = "personId")]
        public int? PersonId { get; set; }

        [DataMember(Name = "cityId")]
        public int CityId { get; set; }

        //[DataMember(Name = "city")]
        //public City City { get; set; }


    }
}
