namespace TheosLivraria.Web.Request.Usuarios
{
    public class CriarUsusarioRequest
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public DateTime DataAniversario { get; set; }
        public int Perfil { get; set; }
    }
}
