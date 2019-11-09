using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    public class MapBase<TEntity, TEntryKey> : IEntityTypeConfiguration<TEntity> where TEntity : EntryBase<TEntryKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Inserted).IsRequired(true);
            builder.Property(x => x.Updated).IsRequired(true);
            builder.Property(x => x.Deleted).IsRequired(false);

            builder.HasQueryFilter(x => x.IsSoftDeleted == false);
        }
    }
}
