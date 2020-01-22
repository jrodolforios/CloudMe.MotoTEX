using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapFaturamento : MapBase<Faturamento, Guid>
    {
        public override void Configure(EntityTypeBuilder<Faturamento> builder)
        {
            base.Configure(builder);

            builder.ToTable("Faturamento");

            builder.Property(x => x.Ano).IsRequired();
            builder.Property(x => x.Mes).IsRequired();
            builder.Property(x => x.Total).IsRequired();

            builder.HasMany(x => x.FaturamentoTaxista).WithOne(x => x.Faturamento).HasForeignKey(x => x.IdFaturamento);
        }
    }
}
