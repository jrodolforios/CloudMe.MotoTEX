using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapGrupoUsuario : MapBase<GrupoUsuario, Guid>
    {
        public override void Configure(EntityTypeBuilder<GrupoUsuario> builder)
        {
            base.Configure(builder);

            builder.ToTable("GrupoUsuario");

            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.Descricao).IsRequired();
        }
    }
}
