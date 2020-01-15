using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapFavorito : MapBase<Favorito, Guid>
    {
        public override void Configure(EntityTypeBuilder<Favorito> builder)
        {
            base.Configure(builder);

            builder.ToTable("Favorito");

            builder.Property(x => x.Preferencia).IsRequired();

            builder.HasOne(x => x.Passageiro).WithMany(x => x.TaxistasFavoritos).HasForeignKey(x => x.IdPassageiro).IsRequired();
            builder.HasOne(x => x.Taxista).WithMany(x => x.Favoritos).HasForeignKey(x => x.IdTaxista).IsRequired();
        }
    }
}
