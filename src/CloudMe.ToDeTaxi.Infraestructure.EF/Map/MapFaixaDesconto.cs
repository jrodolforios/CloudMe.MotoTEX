using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapFaixaDesconto : MapBase<FaixaDesconto, Guid>
    {
        public override void Configure(EntityTypeBuilder<FaixaDesconto> builder)
        {
            base.Configure(builder);

            builder.ToTable("FaixaDesconto");

            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Descricao);
        }
    }
}
