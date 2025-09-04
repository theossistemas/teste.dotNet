namespace TheosLivraria.Web.Util.Response.Ususarios
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Perfil { get; set; }
    }
}
