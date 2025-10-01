using Library.Shared.AppSettings.Types;
using Microsoft.Extensions.Configuration;

namespace Library.Shared.AppSettings;

public static class CustomConfiguration
{
    private readonly static IConfiguration _configuration;

    static CustomConfiguration()
        => _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
    
    public static ConnectionStringsType ConnectionStrings
        => _configuration.GetSection("ConnectionStrings").Get<ConnectionStringsType>()!;

    public static JWTSettingsType JWTSettings
        => _configuration.GetSection("JwtSettings").Get<JWTSettingsType>()!;
}