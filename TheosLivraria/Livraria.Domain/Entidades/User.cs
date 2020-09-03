using FluentValidation;
using Livraria.Common.Utils;

namespace Livraria.Domain.Entidades
{
    public class User : Entity<User>
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        protected User() {}

        public User(string userName, string password, string role)
        {
            Username = userName;
            Password = password;
            Role = role;

        }

        public override bool Validar()
        {
            RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull()
            .MaximumLength(Constantes.QuantidadeDeCaracteres100);

            RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();

            RuleFor(x => x.Role)
            .NotEmpty()
            .NotNull();

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public void AlterarUserName(string userName)
        {
            Username = userName;
        }
        public void AlterarSenha(string senha)
        {
            Password = senha;
        }

        public void AlterarRole(string role)
        {
            Role = role;
        }
    }
}
