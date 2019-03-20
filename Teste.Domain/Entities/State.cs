using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    [DataContract]
    public class State : BaseEntity
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "uf")]
        public string Uf { get; set; }

        public override object[] GetKey()
        {
            return new object[] { Id };
        }
    }
}
