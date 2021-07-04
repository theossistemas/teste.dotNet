namespace Livraria.Services.Interfaces.Administracao
{
    public interface IConfigurationManagerService
    {
        string GetSetting(string key);
        string GetSecurityKey();
    }
}
