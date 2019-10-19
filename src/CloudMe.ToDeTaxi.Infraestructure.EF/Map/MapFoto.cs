using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapFoto : MapBase<Foto>
    {
        public override void Configure(EntityTypeBuilder<Foto> builder)
        {
            base.Configure(builder);

            builder.ToTable("Foto");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired(false);
            builder.Property(x => x.NomeArquivo).IsRequired(false);
            builder.Property(x => x.Dados).IsRequired(false);
        }
    }
}
