using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    public class MapBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntryBase
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.IsDeleted);
            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
