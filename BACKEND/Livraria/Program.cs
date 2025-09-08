using Livraria.Infrastructure;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories;
using Livraria.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Livraria.Service;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true
        },
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning 
    )
    .CreateLogger();

// adicionando services ao container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Livraria API", Version = "v1" });

    // Configuração do Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer. Exemplo: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// adicionando o DbContext antes do Build!
builder.Services.AddDbContext<LivrariaDbContext>(options =>
    options.UseSqlServer(connectionString));

// registro do serviço
builder.Services.AddScoped<ILivroCreateService, LivroCreateService>();
builder.Services.AddScoped<ILivroReadService, LivroReadService>();
builder.Services.AddScoped<ILivroUpdateService, LivroUpdateService>();
builder.Services.AddScoped<ILivroDeleteService, LivroDeleteService>();

builder.Services.AddScoped<ILivroCreateRepository, LivroCreateRepository>();
builder.Services.AddScoped<ILivroReadRepository, LivroReadRepository>();
builder.Services.AddScoped<ILivroUpdateRepository, LivroUpdateRepository>();
builder.Services.AddScoped<ILivroDeleteRepository, LivroDeleteRepository>();

// configuração do JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? "sua-chave-super-secreta";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "LivrariaIssuer";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

// configurando o CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()      // aceita requisições de qualquer origem
              .AllowAnyMethod()      // aceita qualquer método HTTP (GET, POST, etc)
              .AllowAnyHeader();     // aceita qualquer header
    });
});

builder.Host.UseSerilog();

var app = builder.Build();

app.UseCors("CorsPolicy"); // Aplica a política CORS
app.UseAuthentication(); // Adicionando o middleware de autenticação
app.UseAuthorization();

// configurando a HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LivrariaDbContext>();
    SeedData.Initialize(context);
}

app.Run();