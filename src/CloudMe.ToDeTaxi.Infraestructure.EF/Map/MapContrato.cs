using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapContrato : MapBase<Contrato, Guid>
    {
        public override void Configure(EntityTypeBuilder<Contrato> builder)
        {
            base.Configure(builder);

            builder.ToTable("Contrato");

            builder.Property(x => x.Conteudo).IsRequired();
            builder.Property(x => x.UltimaVersao).IsRequired().HasDefaultValue(false);
        }
    }
}
