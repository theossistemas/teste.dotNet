using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain
{
    public class Account: BaseDomain
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
        public string VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public string Password { get; set; }
        public bool RegisterComplete { get; set; }
        
        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.FirstOrDefault(x => x.Token == token) != null;
        }
    }

    public enum Role : int
    {
        Admin = 0,
        User = 1,
    }
}
