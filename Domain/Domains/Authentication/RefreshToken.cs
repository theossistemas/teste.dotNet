using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RefreshToken : BaseDomain
    {
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;

    }
}
