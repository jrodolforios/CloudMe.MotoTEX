using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapVeiculo : MapBase<Veiculo>
    {
        public override void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            base.Configure(builder);

            builder.ToTable("Veiculo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Placa).IsRequired();
            builder.Property(x => x.Modelo).IsRequired();
            builder.Property(x => x.Capacidade).IsRequired();
            builder.Property(x => x.Cor).IsRequired();
            builder.Property(x => x.Foto).IsRequired();
        }
    }
}
