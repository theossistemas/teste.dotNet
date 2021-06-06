using LivrariaWeb.Domain.Interface;
using LivrariaWeb.Infra;
using LivrariaWeb.Infra.Configuration;
using LivrariaWeb.Infra.Repository;
using LivrariaWeb.Service;
using LivrariaWeb.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaWeb.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddControllers();
            services.AddDbContext<ContextBdLivraria>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddMvcCore();

            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var xmlPath = Path.Combine(basePath, "TFCastGroup.Api.xml");


            services.AddSwaggerGen(options =>
            {
                //options.IncludeXmlComments(xmlPath);
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {

                    Title = "Livraria",
                    Version = "v2",
                    Description = "Serviço que disponibiliza soluçoes para estoque de livrarias.",
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
                { 
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference =new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                options.IncludeXmlComments("LivrariaWeb.Api.xml");
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(options => options.RoutePrefix = string.Empty);
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "API Livraria"));
        }
    }
}
