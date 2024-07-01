using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LivrariaJc.Application.Extentions
{
    public static class AutenticationConfig
    {
        public static IServiceCollection AddAutenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = configuration["Jwt:Issuer"],
                       ValidAudience = configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(key)
                   };
               });

            return services;
        }
    }
}
