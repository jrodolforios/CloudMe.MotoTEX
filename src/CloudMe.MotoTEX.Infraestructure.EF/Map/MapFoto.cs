using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapFoto : MapBase<Foto, Guid>
    {
        public override void Configure(EntityTypeBuilder<Foto> builder)
        {
            base.Configure(builder);

            builder.ToTable("Foto");

            builder.Property(x => x.Nome).IsRequired(false);
            builder.Property(x => x.NomeArquivo).IsRequired(false);
            builder.Property(x => x.Dados).IsRequired(false);
        }
    }
}
