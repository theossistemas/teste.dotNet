using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    [DataContract]
    public class User : BaseEntity
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "login")]
        public string Login { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "profile_id")]
        public int ProfileId { get; set; }

        public Profile Profile { get; set; }

        public override object[] GetKey()
        {
            return new object[] { Id };
        }
    }
}
