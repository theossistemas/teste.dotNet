using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impl.Configuration
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            entity.ToTable("PERSON");

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("ID").UseSqlServerIdentityColumn().IsRequired();
            entity.Property(p => p.Name).HasColumnName("NAME").HasMaxLength(100).IsRequired();
            entity.Property(p => p.Cpf).HasColumnName("CPF").HasMaxLength(11).IsRequired();
            entity.Property(p => p.BirthDate).HasColumnName("BIRTH_DATE").IsRequired();
            entity.Property(p => p.GenderId).HasColumnName("GENDER_ID").IsRequired();


            entity.HasOne<Gender>(p => p.Gender).WithMany()
                .HasForeignKey(p => p.GenderId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
