using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapVeiculo : MapBase<Veiculo, Guid>
    {
        public override void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            base.Configure(builder);

            builder.ToTable("Veiculo");

            builder.Property(x => x.Placa).IsRequired();
            builder.Property(x => x.Marca).IsRequired();
            builder.Property(x => x.Modelo).IsRequired();
            builder.Property(x => x.Ano).IsRequired();
            builder.Property(x => x.Versao);
            builder.Property(x => x.Capacidade).IsRequired();
            builder.Property(x => x.Cor);

            builder.HasOne(x => x.Foto).WithOne().HasForeignKey<Veiculo>(x => x.IdFoto);
            builder.HasOne(x => x.CorVeiculo).WithMany().HasForeignKey(x => x.IdCorVeiculo);
        }
    }
}
