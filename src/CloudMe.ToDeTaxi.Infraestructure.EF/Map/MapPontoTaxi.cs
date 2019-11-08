using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapPontoTaxi : MapBase<PontoTaxi, Guid>
    {
        public override void Configure(EntityTypeBuilder<PontoTaxi> builder)
        {
            base.Configure(builder);

            builder.ToTable("PontoTaxi");

            builder.Property(x => x.Nome).IsRequired();

            builder.HasOne(x => x.Endereco).WithOne().HasForeignKey<PontoTaxi>(x => x.IdEndereco).IsRequired();
        }
    }
}
