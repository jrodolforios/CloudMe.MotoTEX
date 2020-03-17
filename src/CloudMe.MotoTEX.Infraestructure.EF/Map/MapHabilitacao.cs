using CloudMe.MotoTEX.Infraestructure.Entries.Piloto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapHabilitacao : MapBase<Habilitacao, Guid>
    {
        public override void Configure(EntityTypeBuilder<Habilitacao> builder)
        {
            base.Configure(builder);

            builder.ToTable("Habilitacao");

            builder.Property(x => x.Categoria).IsRequired();
            builder.Property(x => x.Validade).IsRequired();
            builder.Property(x => x.PrimeiraHabilitacao).IsRequired();

            builder.HasOne(x => x.Taxista).WithOne(x => x.Habilitacao).HasForeignKey<Habilitacao>(x => x.IdTaxista);
            builder.HasOne(x => x.Foto).WithOne().HasForeignKey<Habilitacao>(x => x.IdFoto);
            builder.HasOne(x => x.UF).WithMany().HasForeignKey(x => x.IdUF);
        }
    }
}
