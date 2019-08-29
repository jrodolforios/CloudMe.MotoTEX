﻿using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapFaixaDescontoTaxista : MapBase<FaixaDescontoTaxista>
    {
        public override void Configure(EntityTypeBuilder<FaixaDescontoTaxista> builder)
        {
            base.Configure(builder);

            builder.ToTable("FaixaDescontoTaxista");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.FaixaDesconto).WithMany(x => x.Taxistas).HasForeignKey(x => x.IdFaixaDesconto).IsRequired();
            builder.HasOne(x => x.Taxista).WithMany(x => x.FaixasDesconto).HasForeignKey(x => x.IdTaxista).IsRequired();
        }
    }
}
