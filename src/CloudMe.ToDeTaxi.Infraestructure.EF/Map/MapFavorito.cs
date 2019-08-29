using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapFavorito : MapBase<Favorito>
    {
        public override void Configure(EntityTypeBuilder<Favorito> builder)
        {
            base.Configure(builder);

            builder.ToTable("Favorito");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Preferencia).IsRequired();

            builder.HasOne(x => x.Passageiro).WithMany(x => x.TaxistasFavoritos).HasForeignKey(x => x.IdPassageiro).IsRequired();
            builder.HasOne(x => x.Taxista).WithOne().HasForeignKey<Favorito>(x => x.IdTaxista).IsRequired();
        }
    }
}
