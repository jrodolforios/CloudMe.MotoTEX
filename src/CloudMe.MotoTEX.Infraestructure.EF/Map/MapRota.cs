using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapRota : MapBase<Rota, Guid>
    {
        public override void Configure(EntityTypeBuilder<Rota> builder)
        {
            base.Configure(builder);

            builder.ToTable("Rota");
        }
    }
}
