using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapLocalizacao : MapBase<Localizacao>
    {
        public override void Configure(EntityTypeBuilder<Localizacao> builder)
        {
            base.Configure(builder);

            builder.ToTable("Localizacao");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Endereco).IsRequired(false);
            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.Longitude).IsRequired();
            builder.Property(x => x.NomePublico).IsRequired(false);
        }
    }
}
