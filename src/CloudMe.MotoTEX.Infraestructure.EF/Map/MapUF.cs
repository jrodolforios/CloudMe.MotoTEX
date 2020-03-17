using CloudMe.MotoTEX.Infraestructure.Entries.Localidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    public class MapUF : MapBase<UF, Guid>
    {
        public override void Configure(EntityTypeBuilder<UF> builder)
        {
            base.Configure(builder);

            builder.ToTable("UF");
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.Sigla).IsRequired();
        }
    }
}
