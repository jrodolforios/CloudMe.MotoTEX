using Microsoft.EntityFrameworkCore;
using CloudMe.ToDeTaxi.Infraestructure.EF.Map;
using IdentityServer4.EntityFramework.Options;
using System.Threading;
using System.Threading.Tasks;
using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Contexts
{
    public class CloudMeToDeTaxiContext : CloudMeAdminDbContext
    {
        public CloudMeToDeTaxiContext(
            DbContextOptions<CloudMeToDeTaxiContext> options,
            ConfigurationStoreOptions storeOptions,
            OperationalStoreOptions operationalOptions)
            : base(options, storeOptions, operationalOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.HasDefaultSchema("public");

            builder.ApplyConfiguration(new MapCall());
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is EntryBase)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["IsDeleted"] = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["IsDeleted"] = true;
                            break;
                    }
                }
            }
        }
    }
}
