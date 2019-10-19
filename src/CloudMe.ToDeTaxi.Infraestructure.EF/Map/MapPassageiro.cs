using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapPassageiro: MapBase<Passageiro>
    {
        public override void Configure(EntityTypeBuilder<Passageiro> builder)
        {
            base.Configure(builder);

            builder.ToTable("Passageiro");

            builder.HasKey(x => x.Id);

            // relaxa o relacionamento com o usuario pois este (usuário) é gerenciado externamente (Identity server)
            builder.HasOne(x => x.Usuario).WithOne().HasForeignKey<Passageiro>(x => x.IdUsuario).IsRequired(false);

            builder.HasOne(x => x.Endereco).WithOne().HasForeignKey<Passageiro>(x => x.IdEndereco).IsRequired();
            builder.HasOne(x => x.Foto).WithOne().HasForeignKey<Passageiro>(x => x.IdFoto).IsRequired();
            builder.HasOne(x => x.LocalizacaoAtual).WithOne().HasForeignKey<Passageiro>(x => x.IdLocalizacaoAtual);
        }
    }
}
