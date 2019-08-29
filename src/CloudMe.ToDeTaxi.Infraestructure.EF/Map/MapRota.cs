using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapRota : MapBase<Rota>
    {
        public override void Configure(EntityTypeBuilder<Rota> builder)
        {
            base.Configure(builder);

            builder.ToTable("Rota");

            builder.HasKey(x => x.Id);
        }
    }
}
