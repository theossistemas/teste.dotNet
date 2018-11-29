using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LC.Application.User.DataTransferObject
{
    [DataContract]
    public class TokenDTO
    {
        [DataMember(Name = "authenticated")]
        public Boolean Authenticated { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }


    }
}