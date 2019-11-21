using CloudMe.ToDeTaxi.Domain.Enums;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    public class MapSolicitacaoCorridaTaxista : MapBase<SolicitacaoCorridaTaxista, Guid>
    {
        public override void Configure(EntityTypeBuilder<SolicitacaoCorridaTaxista> builder)
        {
            base.Configure(builder);

            builder.ToTable("SolicitacaoCorridaTaxista");

            builder.Property(x => x.Acao).IsRequired().HasDefaultValue(AcaoTaxistaSolicitacaoCorrida.Indefinido);

            builder.HasOne(x => x.SolicitacaoCorrida).WithMany(x => x.Taxistas).HasForeignKey(x => x.IdSolicitacaoCorrida).IsRequired();
            builder.HasOne(x => x.Taxista).WithMany(x => x.SolicitacoesCorrida).HasForeignKey(x => x.IdTaxista).IsRequired();
        }
    }
}
