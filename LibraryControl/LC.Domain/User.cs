using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LC.Domain
{
    [DataContract]
    public class User : BaseEntity
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "login")]
        public string Login { get; set; }

        [DataMember(Name = "acess_key")]
        public string AcessKey { get; set; }

        public override object[] GetKey()
        {
            return new object[] { Id };
        }
    }
}
