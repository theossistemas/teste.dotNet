using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfissaAPI.Models;

namespace ProfissaAPI.Data.Configuration
{
    public class AlunoConfiguration: IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Aluno");
            builder.HasKey(p => p.Id);
            builder.Property(p=>p.Nome)
        }
    }
}