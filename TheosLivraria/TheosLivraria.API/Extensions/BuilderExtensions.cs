using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using TheosLivraria.Shared.Ioc;

namespace TheosLivraria.API.Extensions
{
    public static class BuilderExtensions
    {
        public static void AdicionarSeguranca(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "Theos",
                            ValidAudience = "TheosAPI",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e1d0a5e8-1d53-499f-b3d8-3468bb95e0b9"))
                        };
                    });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
                options.AddPolicy("Publico", policy => policy.RequireRole("Publico"));
            });
        }

        public static void AdicionarCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
        }

        public static void AdicionarLogging(this WebApplicationBuilder builder)
        {
           
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            builder.Host.UseSerilog((context, services, configurations) =>
                configurations.ReadFrom.Configuration(context.Configuration)
                              .ReadFrom.Services(services)
            );

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
        }

        public static void AdicionarDocumentacao(this WebApplicationBuilder builder)
        {
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "Theos Livraria API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Informe o token JWT no formato: Bearer {seu token}"
                });

                options.AddSecurityRequirement(new()
    {
        {
            new() { Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            Array.Empty<string>()
        }
    });
            });
        }
        
        public static void AdicionarIoc(this WebApplicationBuilder builder)
        {
            IConfiguration configuration = builder.Configuration;
            builder.Services.RegistroDI(configuration);
        }
    }
}
