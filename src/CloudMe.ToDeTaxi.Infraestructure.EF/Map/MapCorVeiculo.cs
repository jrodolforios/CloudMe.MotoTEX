using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    public class MapCorVeiculo : MapBase<CorVeiculo, Guid>
    {
        public override void Configure(EntityTypeBuilder<CorVeiculo> builder)
        {
            base.Configure(builder);

            builder.ToTable("CorVeiculo");

            builder.Property(x => x.Nome).IsRequired();
        }
    }
}
