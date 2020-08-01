namespace Domain.Security
{
    public class TokenConfigurations
    {
        public string Secret { get; set; }
        public int ExpiracaoHoras { get; set; }
        public int ExpiracaoMinutos { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }   
}
