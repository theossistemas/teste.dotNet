using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Theos.Library.Api.Helper;
using Theos.Library.Api.Injection;
using Theos.Library.Api.Mapper;

namespace Theos.Library.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Factory.Build(services);
            MapperConfig.RegisterMappings();
            services.AddMvc()
                .AddJsonOptions(option => option.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Theos Library", Version = "v1" });
                c.OperationFilter<FileUploadOperation>();
                c.DocumentFilter<SecurityRequirementsDocumentFilter>();
                c.AddSecurityDefinition("Authorization",
                    new ApiKeyScheme
                    {
                        Description = "Token received at Login",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey"
                    });

                var application = PlatformServices.Default.Application;
                c.IncludeXmlComments($"{application.ApplicationBasePath}{application.ApplicationName}.xml");
            });

            services.AddCors(o => o.AddPolicy("Policy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Policy");
            app.UseMiddleware(typeof(HandleExceptionHelper));
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Theos Library");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
