namespace MMM.Library.Infra.CrossCutting.Identity.ApiConfiguration
{
    public class TokenSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public string ValidOn { get; set; }
    }
}
