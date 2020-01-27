using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Enums;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    public class MapBase<TEntity, TEntryKey> : IEntityTypeConfiguration<TEntity> where TEntity : EntryBase<TEntryKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Inserted).IsRequired(true);
            builder.Property(x => x.Updated).IsRequired(true);
            builder.Property(x => x.Deleted).IsRequired(false);

            builder.HasQueryFilter(x => !x.IsSoftDeleted);
        }
    }
}
