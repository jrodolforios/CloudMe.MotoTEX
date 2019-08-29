using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapTarifa : MapBase<Tarifa>
    {
        public override void Configure(EntityTypeBuilder<Tarifa> builder)
        {
            base.Configure(builder);

            builder.ToTable("Tarifa");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Bandeirada).IsRequired();
            builder.Property(x => x.KmRodadoBandeira1).IsRequired();
            builder.Property(x => x.KmRodadoBandeira2).IsRequired();
            builder.Property(x => x.HoraParada).IsRequired();
        }
    }
}
