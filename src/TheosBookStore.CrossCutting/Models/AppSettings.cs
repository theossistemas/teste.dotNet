namespace TheosBookStore.CrossCutting.Models
{
    internal class AppSettings
    {
        public string DBHost { get; set; }
        public string DBUsuario { get; set; }
        public string DBSenha { get; set; }
        public string DBName { get; set; }

        public override string ToString()
        {
            return $"DBHost: {DBHost}; DBUsuario: {DBUsuario}; DBSenha: {DBSenha}";
        }
    }
}
