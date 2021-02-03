using JeffersonMello.Livraria.Model.Abstract;

namespace JeffersonMello.Livraria.Model.Cadastro.Pessoa
{
    public class Usuario : EntityBase
    {
        #region Public Properties

        public string Nome { get; set; }

        public string UsuarioAcesso { get; set; }

        public string Senha { get; set; }

        #endregion Public Properties

    }
}