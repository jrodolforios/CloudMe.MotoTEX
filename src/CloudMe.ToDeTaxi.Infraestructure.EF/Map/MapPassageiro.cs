using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapPassageiro: MapBase<Passageiro, Guid>
    {
        public override void Configure(EntityTypeBuilder<Passageiro> builder)
        {
            base.Configure(builder);

            builder.ToTable("Passageiro");

            // relaxa o relacionamento com o usuario pois este (usuário) é gerenciado externamente (Identity server)
            builder.HasOne(x => x.Usuario).WithOne().HasForeignKey<Passageiro>(x => x.IdUsuario).IsRequired(false);

            builder.HasOne(x => x.Endereco).WithOne().HasForeignKey<Passageiro>(x => x.IdEndereco).IsRequired();
            builder.HasOne(x => x.Foto).WithOne().HasForeignKey<Passageiro>(x => x.IdFoto).IsRequired();
            builder.HasOne(x => x.LocalizacaoAtual).WithOne().HasForeignKey<Passageiro>(x => x.IdLocalizacaoAtual);
        }
    }
}
