using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Skoruba.IdentityServer4.Admin.EntityFramework.Entities;
using Skoruba.IdentityServer4.Admin.EntityFramework.Identity.Constants;
using Skoruba.IdentityServer4.Admin.EntityFramework.Identity.Interfaces;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using System;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Contexts {

    public class CloudMeAdminDbContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>,
        IAdminConfigurationDbContext, IAdminLogDbContext, IAdminPersistedGrantDbContext, IAdminPersistedGrantIdentityDbContext
    {
        private readonly ConfigurationStoreOptions _storeOptions;
        private readonly OperationalStoreOptions _operationalOptions;

        public CloudMeAdminDbContext(DbContextOptions options,
            ConfigurationStoreOptions storeOptions,
                OperationalStoreOptions operationalOptions)
            : base(options)
        {
            _storeOptions = storeOptions;
            _operationalOptions = operationalOptions;
        }

        public DbSet<ApiResource> ApiResources { get; set; }

        public DbSet<ApiResourceProperty> ApiResourceProperties { get; set; }

        public DbSet<IdentityResource> IdentityResources { get; set; }

        public DbSet<IdentityResourceProperty> IdentityResourceProperties { get; set; }

        public DbSet<ApiSecret> ApiSecrets { get; set; }

        public DbSet<ApiScope> ApiScopes { get; set; }

        public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }

        public DbSet<IdentityClaim> IdentityClaims { get; set; }

        public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientGrantType> ClientGrantTypes { get; set; }

        public DbSet<ClientScope> ClientScopes { get; set; }

        public DbSet<ClientSecret> ClientSecrets { get; set; }

        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }

        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }

        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }

        public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }

        public DbSet<ClientClaim> ClientClaims { get; set; }

        public DbSet<ClientProperty> ClientProperties { get; set; }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        public DbSet<Log> Logs { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureIdentityContext(builder);
            ConfigureLogContext(builder);
            builder.ConfigureClientContext(_storeOptions);
            builder.ConfigureResourcesContext(_storeOptions);
            builder.ConfigurePersistedGrantContext(_operationalOptions);
        }

        private void ConfigureLogContext(ModelBuilder builder)
        {
            builder.Entity<Log>(log =>
            {
                log.ToTable(Skoruba.IdentityServer4.Admin.EntityFramework.Constants.TableConsts.Logging);
                log.HasKey(x => x.Id);
                log.Property(x => x.Level).HasMaxLength(128);
            });
        }

        private void ConfigureIdentityContext(ModelBuilder builder)
        {
            builder.Entity<IdentityRole<Guid>>().ToTable(TableConsts.IdentityRoles);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable(TableConsts.IdentityRoleClaims);
            builder.Entity<IdentityUserRole<Guid>>().ToTable(TableConsts.IdentityUserRoles);

            builder.Entity<Usuario>().ToTable(TableConsts.IdentityUsers);
            builder.Entity<IdentityUserLogin<Guid>>().ToTable(TableConsts.IdentityUserLogins);
            builder.Entity<IdentityUserClaim<Guid>>().ToTable(TableConsts.IdentityUserClaims);
            builder.Entity<IdentityUserToken<Guid>>().ToTable(TableConsts.IdentityUserTokens);
        }
    }
}
