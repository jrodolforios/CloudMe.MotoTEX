using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapFormaPagamento : MapBase<FormaPagamento>
    {
        public override void Configure(EntityTypeBuilder<FormaPagamento> builder)
        {
            base.Configure(builder);

            builder.ToTable("FormaPagamento");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descricao).IsRequired();
        }
    }
}
