using System.Collections.Generic;

using TheosBookStore.Auth.Domain.ValueObjects;
using TheosBookStore.LibCommon.Entities;

namespace TheosBookStore.Auth.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public Password Password { get; protected set; }
        public string Roles { get; protected set; }

        private User() { }
        public User(string name, string email, Password password, string roles) : this()
        {
            Name = name;
            Email = email;
            Password = new Password(password.Hash, password.Salt);
            Roles = roles;
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }

        public bool IsValidCredentials(Credentials credentials)
        {
            var userCredentials = new Credentials(this.Email, this.Password);
            return userCredentials.Equals(credentials);
        }
    }
}
