using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapGrupoUsuario : MapBase<GrupoUsuario>
    {
        public override void Configure(EntityTypeBuilder<GrupoUsuario> builder)
        {
            base.Configure(builder);

            builder.ToTable("GrupoUsuario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.Descricao).IsRequired();
        }
    }
}
