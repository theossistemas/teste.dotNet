using System;

namespace Theos.Library.Api.Security
{
    public class UserInfo
    {
        public UserInfo(Guid id, Guid token, ProfileEnum profile)
        {
            Id = id;
            Token = token;
            LastConnection = DateTime.Now;
            Profile = profile;
        }

        public Guid Id { get; set; }
        public Guid Token { get; set; }
        public Guid BranchId { get; set; }
        public DateTime LastConnection { get; set; }
        public ProfileEnum Profile { get; set; }
    }
}
