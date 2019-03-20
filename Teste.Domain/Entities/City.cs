using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    [DataContract]
    public class City : BaseEntity
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "state_id")]
        public int StateId { get; set; }

        [DataMember(Name = "state")]
        public virtual State State { get; set; }

        public override object[] GetKey()
        {
            return new object[] { Id };
        }
    }
}
