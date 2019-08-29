using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapFaixaDesconto : MapBase<FaixaDesconto>
    {
        public override void Configure(EntityTypeBuilder<FaixaDesconto> builder)
        {
            base.Configure(builder);

            builder.ToTable("FaixaDesconto");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Descricao);
        }
    }
}
