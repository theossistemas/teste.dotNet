namespace LivrariaWeb.Domain.Model
{
    public class Pessoa
    {
        public Pessoa()
        {

        }

        public Pessoa(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}