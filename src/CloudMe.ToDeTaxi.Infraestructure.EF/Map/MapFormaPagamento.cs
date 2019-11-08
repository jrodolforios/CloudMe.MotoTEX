using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapFormaPagamento : MapBase<FormaPagamento, Guid>
    {
        public override void Configure(EntityTypeBuilder<FormaPagamento> builder)
        {
            base.Configure(builder);

            builder.ToTable("FormaPagamento");

            builder.Property(x => x.Descricao).IsRequired();
        }
    }
}
