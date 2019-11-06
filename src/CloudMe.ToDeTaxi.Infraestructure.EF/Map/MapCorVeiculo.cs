using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    public class MapCorVeiculo : MapBase<CorVeiculo>
    {
        public override void Configure(EntityTypeBuilder<CorVeiculo> builder)
        {
            base.Configure(builder);

            builder.ToTable("CorVeiculo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired();
        }
    }
}
