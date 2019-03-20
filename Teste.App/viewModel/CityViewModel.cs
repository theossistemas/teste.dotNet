using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Teste.App.viewModel
{
    public class CityViewModel
    {
        [DataMember(Name = "id")]
        public int Id{ get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "state_id")]
        public int StateId { get; set; }

        [DataMember(Name = "State")]
        public StateViewModel State { get; set; }
    }
}
