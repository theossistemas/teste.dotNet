using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Gp.CrossCutting.Dependencies;
using LivrariaJc.CrossCutting.Dependencies;
using LivrariaJc.CrossCutting.Extetions;
using LivrariaJc.Application.Extentions;
using System;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddSingleton(Log.Logger);
builder.Services.ResolveData(config);
builder.Services.ResolveInjectDependencies(config);
builder.Services.ResolveMapperConfigration(config);
builder.Services.AddAutenticationConfiguration(config);
builder.Services.AddSwaggerConfiguration(config);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
Log.Information("System - Serviços inicializados..." );


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Livraria Jc");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors(options => options
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
     );
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    app.Run();
}
catch (Exception e)
{
    Log.Error("System - Falha inesperada durante a execução: " + e.Message);
}
finally
{
    Log.Information("System - Serviço finalizado...");
}
