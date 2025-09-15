using Livraria.API.Extensions;
using Livraria.API.Middlewares;
using Livraria.Infrastructure.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configuração de Log
Logger.Configure();
builder.Host.UseSerilog();

// Serviços da aplicação
builder.Services.AddApplicationServices(builder.Configuration);   // Serviços/Repos
builder.Services.AddJwtConfiguration(builder.Configuration);      // JWT/Auth
builder.Services.AddSwaggerConfiguration();                       // Swagger
builder.Services.AddDatabaseConfiguration(builder.Configuration); // Dapper/EF
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAngular");

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
