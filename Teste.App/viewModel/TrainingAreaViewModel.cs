using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Teste.App.viewModel
{
    [DataContract]
    public class TrainingAreaViewModel
    {
        [DataMember(Name = "id")]
        public int? Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "year_init")]
        public DateTime YearInit { get; set; }

        [DataMember(Name = "year_finish")]
        public DateTime YearFinish { get; set; }

        [DataMember(Name = "college")]
        public string College { get; set; }


    }
}
