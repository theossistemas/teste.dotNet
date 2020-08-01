using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UsuarioModel
    {
        public class RegistrarUsuario
        {
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            [EmailAddress(ErrorMessage = "O Campo {0} está no formato inválido")]
            public string Email { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string Nome { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string Sobrenome { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string Telefone { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            [StringLength(100, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracter", MinimumLength = 8)]
            public string Senha { get; set; }
            [Compare("Senha", ErrorMessage = "As Senhas não Conferem")]
            public string ConfirmaSenha { get; set; }
        }
        public class AtualizarUsuario
        {
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string Id { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            [EmailAddress(ErrorMessage = "O Campo {0} está no formato inválido")]
            public string Email { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string Nome { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string Sobrenome { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string Telefone { get; set; }
            public string Senha { get; set; }
        }
        public class UsuarioGet
        {
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string Id { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            [EmailAddress(ErrorMessage = "O Campo {0} está no formato inválido")]
            public string Email { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string Nome { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string Sobrenome { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string Telefone { get; set; }
            public bool Ativo { get; set; }
        }

        public class LoginUsuario
        {
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "O Campo {0} é obrigatório")]
            [StringLength(100, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracter", MinimumLength = 8)]
            public string Senha { get; set; }
        }

        public class UsuarioDto
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Nome { get; set; }
            public string Sobrenome { get; set; }
            public string Telefone { get; set; }
            public string Token { get; set; }
        }
        public class LoginUsuarioDto
        {
            public string Jwt { get; set; }
        }
    }
}

