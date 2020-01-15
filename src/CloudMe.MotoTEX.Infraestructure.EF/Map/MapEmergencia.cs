using CloudMe.MotoTEX.Domain.Enums;
using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapEmergencia : MapBase<Emergencia, Guid>
    {
        public override void Configure(EntityTypeBuilder<Emergencia> builder)
        {
            base.Configure(builder);

            builder.ToTable("Emergencia");

            builder.Property(x => x.Status).IsRequired().HasDefaultValue(StatusEmergencia.Indefinido);

            builder.HasOne(x => x.Taxista).WithMany().HasForeignKey(x => x.IdTaxista);

            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.Longitude).IsRequired();
        }
    }
}
