using Microsoft.EntityFrameworkCore;
using CloudMe.MotoTEX.Infraestructure.EF.Map;
using IdentityServer4.EntityFramework.Options;
using System.Threading;
using System.Threading.Tasks;
using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using EntityFrameworkCore.Triggers;
using Microsoft.Extensions.DependencyInjection;

namespace CloudMe.MotoTEX.Infraestructure.EF.Contexts
{
    public class CloudMeMotoTEXContext : CloudMeAdminDbContext
    {
        private readonly IServiceProvider serviceProvider;

        public DbSet<Corrida> Corridas { get; set; }
        public DbSet<FaixaDesconto> FaixasDesconto { get; set; }
        public DbSet<FaixaDescontoTaxista> FaixasDescontoTaxistas { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<FormaPagamento> FormasPagamento { get; set; }
        public DbSet<FormaPagamentoTaxista> FormasPagamentoTaxistas { get; set; }
        public DbSet<GrupoUsuario> GruposUsuario { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<Localizacao> Enderecos { get; set; }
        public DbSet<Passageiro> Passageiros { get; set; }
        public DbSet<Rota> Rotas { get; set; }
        public DbSet<SolicitacaoCorrida> SolicitacoesCorrida { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<Taxista> Taxistas { get; set; }
        public DbSet<PontoTaxi> PontosTaxi { get; set; }
        public DbSet<UsuarioGrupoUsuario> UsuariosGruposUsuarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<VeiculoTaxista> VeiculosTaxistas { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<CorVeiculo> CoresVeiculos { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<SolicitacaoCorridaTaxista> SolicitacoesCorridaTaxistas { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<MensagemDestinatario> MensagensDestinatarios  { get; set; }
        public DbSet<Emergencia> Emergencias { get; set; }
        public DbSet<Contato> Contatos { get; set; }


        public CloudMeMotoTEXContext(
            DbContextOptions<CloudMeMotoTEXContext> options,
            ConfigurationStoreOptions storeOptions,
            OperationalStoreOptions operationalOptions,
            IServiceProvider serviceProvider)
            : base(options, storeOptions, operationalOptions)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.HasDefaultSchema("public");

            //builder.ApplyConfiguration(new MapCall());
            builder.ApplyConfiguration(new MapCorrida());
            builder.ApplyConfiguration(new MapFaixaDesconto());
            builder.ApplyConfiguration(new MapFaixaDescontoTaxista());
            builder.ApplyConfiguration(new MapFavorito());
            builder.ApplyConfiguration(new MapFormaPagamento());
            builder.ApplyConfiguration(new MapFormaPagamentoTaxista());
            builder.ApplyConfiguration(new MapGrupoUsuario());
            builder.ApplyConfiguration(new MapLocalizacao());
            builder.ApplyConfiguration(new MapEndereco());
            builder.ApplyConfiguration(new MapPassageiro());
            builder.ApplyConfiguration(new MapRota());
            builder.ApplyConfiguration(new MapSolicitacaoCorrida());
            builder.ApplyConfiguration(new MapTarifa());
            builder.ApplyConfiguration(new MapTaxista());
            builder.ApplyConfiguration(new MapPontoTaxi());
            builder.ApplyConfiguration(new MapUsuarioGrupoUsuario());
            builder.ApplyConfiguration(new MapVeiculo());
            builder.ApplyConfiguration(new MapVeiculoTaxista());
            builder.ApplyConfiguration(new MapFoto());
            builder.ApplyConfiguration(new MapCorVeiculo());
            builder.ApplyConfiguration(new MapContrato());
            builder.ApplyConfiguration(new MapSolicitacaoCorridaTaxista());
            builder.ApplyConfiguration(new MapMensagem());
            builder.ApplyConfiguration(new MapMensagemDestinatario());
            builder.ApplyConfiguration(new MapContato());
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            //UpdateSoftDeleteStatuses();
            //return base.SaveChanges();
            return this.SaveChangesWithTriggers(base.SaveChanges, serviceProvider, acceptAllChangesOnSuccess: true);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            //UpdateSoftDeleteStatuses();
            //return base.SaveChanges(acceptAllChangesOnSuccess);
            return this.SaveChangesWithTriggers(base.SaveChanges, serviceProvider, acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            //UpdateSoftDeleteStatuses();
            //return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, serviceProvider, acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //UpdateSoftDeleteStatuses();
            //return base.SaveChangesAsync(cancellationToken);
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, serviceProvider, acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);
        }

        /*private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is EntryBase)
                {
                    var entryBase = (EntryBase)entry.Entity;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entryBase.IsDeleted = false;
                            //entry.CurrentValues["IsDeleted"] = false;
                            break;
                        case EntityState.Deleted:
                            if (!entryBase.ForceDelete)
                            {
                                entryBase.IsDeleted = true;
                                entry.State = EntityState.Modified;
                                //entry.CurrentValues["IsDeleted"] = true;
                            }
                            break;
                    }
                }
            }
        }*/
    }
}
