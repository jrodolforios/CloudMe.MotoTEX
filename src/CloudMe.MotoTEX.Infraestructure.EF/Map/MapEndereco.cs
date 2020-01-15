using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapEndereco : MapBase<Endereco, Guid>
    {
        public override void Configure(EntityTypeBuilder<Endereco> builder)
        {
            base.Configure(builder);

            builder.ToTable("Endereco");

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
