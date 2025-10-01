using Audit.Core;
using Audit.EntityFramework;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.AppDbContext;

public class DataContext(DbContextOptions<DataContext> options) : AuditDbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Login> Users { get; set; }
    public DbSet<AuditLog> AuditLog { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

    public override void OnScopeCreated(IAuditScope auditScope)
    {
        if (auditScope.Event is AuditEventEntityFramework ev)
        {
            var tables = ev.EntityFrameworkEvent.Entries
                          .Select(e => e.Table)
                          .Where(t => !string.IsNullOrEmpty(t))
                          .Distinct()
                          .ToArray();

            auditScope.SetCustomField("TableName", string.Join(",", tables));
        }

        base.OnScopeCreated(auditScope);
    }
}