using Audit.SqlServer;
using Audit.SqlServer.Providers;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infraestructure.AppDbContext;
using Library.Infraestructure.Repositories;
using Library.Shared.AppSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.IoC.Infraestructure;

public static class InfraestructureIoC
{
    public static IServiceCollection AddInfraestruucture(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddHttpContextAccessor();
        services.AddDataContext();
#if !TEST
        // Só roda em produção/release/dev, nunca em testes unitários
        AuditLogsConfiguration();
#endif
        return services;
    }

    public static IServiceCollection AddDataContext(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(CustomConfiguration.ConnectionStrings.DefaultConnection));

        return services;
    }

    private static void AuditLogsConfiguration()
    {
        IgnoreSensitivyProperties();

        Audit.Core.Configuration.DataProvider = new SqlDataProvider()
        {
            ConnectionString = CustomConfiguration.ConnectionStrings.DefaultConnection,
            TableName = "AUDIT_LOGS",
            IdColumnName = "ID",
            JsonColumnName = "DATA",
            CustomColumns = new List<CustomColumn>()
            {
                new CustomColumn("EVENT_TYPE", ev => ev.EventType),
                new CustomColumn("USER_NAME", ev => ev.Environment.UserName),
                new CustomColumn("TABLE_NAME", ev => ev.CustomFields.ContainsKey("TableName") ? ev.CustomFields["TableName"] : "Nome da tabela não encontrada"),
                new CustomColumn("CREATED_AT", ev => DateTime.UtcNow)
            }
        };
    }

    private static void IgnoreSensitivyProperties()
    {
        Audit.EntityFramework.Configuration.Setup()
            .ForContext<DataContext>(config => config
                .ForEntity<Login>(entity => entity
                    .Ignore(x => x.PasswordHash)
        ));
    }
}