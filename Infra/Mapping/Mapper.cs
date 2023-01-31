using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Mapping
{
    internal class Mapper
    {
    }

    public class FreteMap : IEntityTypeConfiguration<Frete>
    {
        public void Configure(EntityTypeBuilder<Frete> builder)
        {
            builder.ToTable("Frete");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Origin)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("varchar(100)");

        }
    }
}
