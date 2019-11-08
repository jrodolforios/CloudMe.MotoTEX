using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapTaxista : MapBase<Taxista>
    {
        public override void Configure(EntityTypeBuilder<Taxista> builder)
        {
            base.Configure(builder);

            builder.ToTable("Taxista");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Disponivel).IsRequired().HasDefaultValue(false);

             // relaxa o relacionamento com o usuario pois este (usuário) é gerenciado externamente (Identity server)
            builder.HasOne(x => x.Usuario).WithOne().HasForeignKey<Taxista>(x => x.IdUsuario).IsRequired(false);

            builder.HasOne(x => x.Endereco).WithOne().HasForeignKey<Taxista>(x => x.IdEndereco).IsRequired();
            builder.HasOne(x => x.Foto).WithOne().HasForeignKey<Taxista>(x => x.IdFoto).IsRequired();
            builder.HasOne(x => x.LocalizacaoAtual).WithOne().HasForeignKey<Taxista>(x => x.IdLocalizacaoAtual);
            builder.HasOne(x => x.PontoTaxi).WithMany(x => x.Taxistas).HasForeignKey(x => x.IdPontoTaxi);
        }
    }
}
