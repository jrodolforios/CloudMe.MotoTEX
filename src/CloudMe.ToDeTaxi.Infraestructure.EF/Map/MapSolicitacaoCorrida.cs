using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapSolicitacaoCorrida : MapBase<SolicitacaoCorrida, Guid>
    {
        public override void Configure(EntityTypeBuilder<SolicitacaoCorrida> builder)
        {
            base.Configure(builder);

            builder.ToTable("SolicitacaoCorrida");

            builder.Property(x => x.TipoAtendimento).IsRequired().HasDefaultValue(TipoAtendimento.Indefinido);
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.ETA).IsRequired();
            builder.Property(x => x.TempoDisponivel).IsRequired(false);
            builder.Property(x => x.ValorEstimado).IsRequired(false);
            builder.Property(x => x.ValorProposto).IsRequired(false);
            builder.Property(x => x.Situacao).IsRequired().HasDefaultValue(SituacaoSolicitacaoCorrida.Indefinido);
            builder.Property(x => x.StatusMonitoramento).IsRequired().HasDefaultValue(StatusMonitoramentoSolicitacaoCorrida.Indefinido);

            builder.HasOne(x => x.Passageiro).WithMany(x => x.SolicitacoesCorrida).HasForeignKey(x => x.IdPassageiro);
            builder.HasOne(x => x.LocalizacaoOrigem).WithMany().HasForeignKey(x => x.IdLocalizacaoOrigem);
            builder.HasOne(x => x.LocalizacaoDestino).WithMany().HasForeignKey(x => x.IdLocalizacaoDestino);
            builder.HasOne(x => x.Rota).WithMany().HasForeignKey(x => x.IdRota);
            builder.HasOne(x => x.FormaPagamento).WithMany().HasForeignKey(x => x.IdFormaPagamento);
            builder.HasOne(x => x.FaixaDesconto).WithMany().HasForeignKey(x => x.IdFaixaDesconto);
        }
    }
}
