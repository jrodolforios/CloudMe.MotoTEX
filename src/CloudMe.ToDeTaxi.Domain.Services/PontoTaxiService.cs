using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using CloudMe.ToDeTaxi.Domain.Model;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class PontoTaxiService : ServiceBase<PontoTaxi, PontoTaxiSummary, Guid>, IPontoTaxiService
    {
        private string[] defaultPaths = {"Endereco"};

        private readonly IPontoTaxiRepository _PontoTaxiRepository;

        public PontoTaxiService(IPontoTaxiRepository PontoTaxiRepository)
        {
            _PontoTaxiRepository = PontoTaxiRepository;
        }

        protected override Task<PontoTaxi> CreateEntryAsync(PontoTaxiSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var PontoTaxi = new PontoTaxi
            {
                Id = summary.Id,
                Nome = summary.Nome,
                IdEndereco = summary.Endereco.Id,
            };
            return Task.FromResult(PontoTaxi);
        }

        protected override Task<PontoTaxiSummary> CreateSummaryAsync(PontoTaxi entry)
        {
            var PontoTaxi = new PontoTaxiSummary
            {
                Id = entry.Id,
                Nome = entry.Nome,
                Endereco = new LocalizacaoSummary()
                {
                    Id = entry.Endereco.Id,
                    Endereco = entry.Endereco.Endereco,
                    NomePublico = entry.Endereco.NomePublico,
                    Latitude = entry.Endereco.Latitude,
                    Longitude = entry.Endereco.Longitude
                }
            };

            return Task.FromResult(PontoTaxi);
        }

        protected override Guid GetKeyFromSummary(PontoTaxiSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<PontoTaxi> GetRepository()
        {
            return _PontoTaxiRepository;
        }

        protected override void UpdateEntry(PontoTaxi entry, PontoTaxiSummary summary)
        {
            entry.Nome = summary.Nome;
            entry.IdEndereco = summary.Endereco.Id;
        }

        protected override void ValidateSummary(PontoTaxiSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "PontoTaxi: sumário é obrigatório"));
            }

            if (String.IsNullOrEmpty(summary.Nome))
            {
                this.AddNotification(new Notification("Nome", "PontoTaxi: nome não fornecido"));
            }
        }

        public override async Task<PontoTaxi> Get(Guid key, string[] paths = null)
        {
            return await base.Get(key, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override async Task<IEnumerable<PontoTaxi>> GetAll(string[] paths = null)
        {
            return await base.GetAll(paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override IEnumerable<PontoTaxi> Search(Expression<Func<PontoTaxi, bool>> where, string[] paths = null, SearchOptions options = null)
        {
            return base.Search(where, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths, options);
        }
    }
}
