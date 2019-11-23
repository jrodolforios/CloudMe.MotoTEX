using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapCorrida : MapBase<Corrida, Guid>
    {
        public override void Configure(EntityTypeBuilder<Corrida> builder)
        {
            base.Configure(builder);

            builder.ToTable("Corrida");

            builder.Property(x => x.Inicio).IsRequired(false);
            builder.Property(x => x.Fim).IsRequired(false);
            builder.Property(x => x.UltimaPausa).IsRequired(false);
            builder.Property(x => x.AvaliacaoTaxista).IsRequired(false).HasDefaultValue(AvaliacaoUsuario.Indefinido);
            builder.Property(x => x.AvaliacaoPassageiro).IsRequired(false).HasDefaultValue(AvaliacaoUsuario.Indefinido);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(StatusCorrida.Indefinido);
            builder.Property(x => x.TempoEmEspera).IsRequired().HasDefaultValue(0);

            builder.HasOne(x => x.Solicitacao).WithOne(x => x.Corrida).HasForeignKey<Corrida>(x => x.IdSolicitacao);
            builder.HasOne(x => x.Taxista).WithMany(x => x.Corridas).HasForeignKey(x => x.IdTaxista);
            builder.HasOne(x => x.Veiculo).WithMany(x => x.Corridas).HasForeignKey(x => x.IdVeiculo);
            builder.HasOne(x => x.RotaExecutada).WithMany().HasForeignKey(x => x.IdRotaExecutada);
            builder.HasOne(x => x.Tarifa).WithMany().HasForeignKey(x => x.IdTarifa);
        }
    }
}
