using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapFormaPagamentoTaxista : MapBase<FormaPagamentoTaxista>
    {
        public override void Configure(EntityTypeBuilder<FormaPagamentoTaxista> builder)
        {
            base.Configure(builder);

            builder.ToTable("FormaPagamentoTaxista");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.FormaPagamento).WithMany(x => x.Taxistas).HasForeignKey(x => x.IdFormaPagamento).IsRequired();
            builder.HasOne(x => x.Taxista).WithMany(x => x.FormasPagamento).HasForeignKey(x => x.IdTaxista).IsRequired();
        }
    }
}
