using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    [DataContract]
    public class TrainingArea : BaseEntity
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "year_init")]
        public DateTime YearInit { get; set; }

        [DataMember(Name = "year_finish")]
        public DateTime YearFinish { get; set; }

        [DataMember(Name = "college")]
        public string College { get; set; }

        public override object[] GetKey()
        {
            return new object[] { Id };
        }
    }
}
