using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapFormaPagamentoTaxista : MapBase<FormaPagamentoTaxista, Guid>
    {
        public override void Configure(EntityTypeBuilder<FormaPagamentoTaxista> builder)
        {
            base.Configure(builder);

            builder.ToTable("FormaPagamentoTaxista");

            builder.HasOne(x => x.FormaPagamento).WithMany(x => x.Taxistas).HasForeignKey(x => x.IdFormaPagamento).IsRequired();
            builder.HasOne(x => x.Taxista).WithMany(x => x.FormasPagamento).HasForeignKey(x => x.IdTaxista).IsRequired();
        }
    }
}
