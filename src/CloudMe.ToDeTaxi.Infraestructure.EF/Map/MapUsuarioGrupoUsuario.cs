using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapUsuarioGrupoUsuario : MapBase<UsuarioGrupoUsuario, Guid>
    {
        public override void Configure(EntityTypeBuilder<UsuarioGrupoUsuario> builder)
        {
            base.Configure(builder);

            builder.ToTable("UsuarioGrupoUsuario");

            builder.HasOne(x => x.Usuario).WithMany(x => x.Grupos).HasForeignKey(x => x.IdUsuario).IsRequired();
            builder.HasOne(x => x.GrupoUsuario).WithMany(x => x.Usuarios).HasForeignKey(x => x.IdGrupoUsuario).IsRequired();
        }
    }
}
