using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapFaturamentoTaxista : MapBase<FaturamentoTaxista, Guid>
    {
        public override void Configure(EntityTypeBuilder<FaturamentoTaxista> builder)
        {
            base.Configure(builder);

            builder.ToTable("FaturamentoTaxista");

            builder.Property(x => x.IdFaturamento).IsRequired();
            builder.Property(x => x.Total).IsRequired();
            builder.Property(x => x.IdTaxista).IsRequired();

            builder.HasMany(x => x.Corrida).WithOne(x => x.FaturamentoTaxista).HasForeignKey(x => x.IdFaturamentoTaxista);
            builder.HasOne(x => x.Taxista).WithMany(x => x.FaturamentoTaxista).HasForeignKey(x => x.IdTaxista);
            builder.HasOne(x => x.Faturamento).WithMany(x => x.FaturamentoTaxista).HasForeignKey(x => x.IdFaturamento);
        }
    }
}
