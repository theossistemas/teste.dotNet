using Livraria.Services.Interfaces.Administracao;
using Microsoft.Extensions.Configuration;

namespace Livraria.Services.Administracao
{
    public class ConfigurationManagerService : IConfigurationManagerService
    {
        private readonly IConfiguration _configuration;
        public ConfigurationManagerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetSetting(string key)
        {
            return _configuration[key];
        }

        public string GetSecurityKey()
        {
            return GetSetting("SecurityKey");
        }
    }
}
