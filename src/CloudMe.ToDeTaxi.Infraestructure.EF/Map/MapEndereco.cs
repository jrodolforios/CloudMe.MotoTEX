using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapEndereco : MapBase<Endereco>
    {
        public override void Configure(EntityTypeBuilder<Endereco> builder)
        {
            base.Configure(builder);

            builder.ToTable("Endereco");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CEP).IsRequired();
            builder.Property(x => x.Logradouro).IsRequired();
            builder.Property(x => x.Numero).IsRequired();
            builder.Property(x => x.Bairro).IsRequired();
            builder.Property(x => x.Localidade).IsRequired();
            builder.Property(x => x.UF).IsRequired();

            builder.HasOne(x => x.Localizacao).WithOne().HasForeignKey<Endereco>(x => x.IdLocalizacao);
        }
    }
}
