using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    public class MapCall : MapBase<Call>
    {
        public override void Configure(EntityTypeBuilder<Call> builder)
        {
            base.Configure(builder);

            builder.ToTable("Call");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CPF).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Number).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(CallStatus.Undefined);
            builder.Property(x => x.Type).IsRequired().HasDefaultValue(CallType.Undefined);
            builder.Property(x => x.Start).IsRequired();
            builder.Property(x => x.End).IsRequired();
            builder.Property(x => x.ExtensionId).IsRequired(false);
        }
    }
}
