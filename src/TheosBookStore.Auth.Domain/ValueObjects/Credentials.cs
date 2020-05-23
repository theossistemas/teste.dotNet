
using TheosBookStore.LibCommon.ValueObjects;

namespace TheosBookStore.Auth.Domain.ValueObjects
{
    public class Credentials : ValueObject<Credentials>
    {
        public string Email { get; private set; }
        public Password Password { get; private set; }

        public Credentials(string email, string password, byte[] salt = null)
        {
            Email = email;
            Password = new Password(password, salt);
        }

        public Credentials(string email, Password password)
        {
            Email = email;
            Password = password;
        }

        protected override bool EqualsCore(Credentials other)
        {
            return this.Email.Equals(other.Email) &&
                this.Password.Equals(other.Password);
        }

        protected override int GetHashCodeCore()
        {
            return (GetType().GetHashCode() * 42) +
                this.Email.GetHashCode() + this.Password.GetHashCode();
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Email) && Password.IsValid();
        }
    }
}
