using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impl.Configuration
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entity)
        {
            entity.ToTable("ADDRESS");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Street).HasColumnName("STREET").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Cep).HasColumnName("CEP").HasMaxLength(16).IsRequired();
            entity.Property(e => e.Neighborhood).HasColumnName("NEIGHBORHOOD").HasMaxLength(30).IsRequired();
            entity.Property(e => e.Number).HasColumnName("NUMBER").IsRequired();

            entity.Property(e => e.PersonId).HasColumnName("PESSOA_ID").IsRequired(false);

            entity.Property(e => e.CityId).HasColumnName("CITY_ID").IsRequired();
            entity.HasOne<City>(e => e.City).WithMany().HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
