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

    public class FreightMap : IEntityTypeConfiguration<Freight>
    {
        public void Configure(EntityTypeBuilder<Freight> builder)
        {
            builder.ToTable("Freight");

            builder.HasKey(prop => prop.Id);

        }  
        public void Configure(EntityTypeBuilder <FreightPrice> builder)
        {
            builder.ToTable("FreightPrice");

            builder.HasKey(prop => prop.Id);

        }
    }
}
