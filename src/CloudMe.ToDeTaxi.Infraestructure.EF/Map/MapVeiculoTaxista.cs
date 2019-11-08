using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapVeiculoTaxista : MapBase<VeiculoTaxista, Guid>
    {
        public override void Configure(EntityTypeBuilder<VeiculoTaxista> builder)
        {
            base.Configure(builder);

            builder.ToTable("VeiculoTaxista");

            builder.HasOne(x => x.Veiculo).WithMany(x => x.Taxistas).HasForeignKey(x => x.IdVeiculo).IsRequired();
            builder.HasOne(x => x.Taxista).WithMany(x => x.Veiculos).HasForeignKey(x => x.IdTaxista).IsRequired();
        }
    }
}
