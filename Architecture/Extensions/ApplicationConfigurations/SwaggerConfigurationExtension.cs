using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture
{
    public static class SwaggerConfigurationExtension
    {
		public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
		{
			services.AddSwaggerGenNewtonsoftSupport();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1",
					new OpenApiInfo
					{
						Title = "Book Store",
						Version = "v1",
						Description = "",
						//Contact = new OpenApiContact
						//{
						//	Email = "",
						//	Name = "",
						//	Url = new Uri("")
						//}
					});
			});

			return services;
		}

		public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
#if DEBUG
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
#else
				c.SwaggerEndpoint("/BookStore/swagger/v1/swagger.json", "v1");
#endif
			});

			return app;
		}
	}
}
