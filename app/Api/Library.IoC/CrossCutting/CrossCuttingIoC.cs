using System.Text;
using Library.Domain.Enum;
using Library.CrossCutting.Auth;
using Library.Shared.AppSettings;
using Microsoft.IdentityModel.Tokens;
using Library.CrossCutting.Auth.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Library.IoC.CrossCutting;

public static class CrossCuttingIoC
{
    public static IServiceCollection AddCrossCutting(this IServiceCollection services)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(CustomConfiguration.JWTSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Administrator", policy => policy.RequireRole(Roles.Admin.ToString()));
        });

        AddServices(services);

        return services;
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
    }
}
