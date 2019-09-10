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
            builder.Property(x => x.CPF).HasMaxLength(20).IsRequired();

            builder.HasOne(x => x.Usuario).WithOne().HasForeignKey<Passageiro>(x => x.IdUsuario).IsRequired();
            builder.HasOne(x => x.Endereco).WithOne().HasForeignKey<Passageiro>(x => x.IdEndereco).IsRequired();
            builder.HasOne(x => x.Foto).WithOne().HasForeignKey<Passageiro>(x => x.IdFoto);
            builder.HasOne(x => x.LocalizacaoAtual).WithOne().HasForeignKey<Passageiro>(x => x.IdLocalizacaoAtual);
        }
    }
}
