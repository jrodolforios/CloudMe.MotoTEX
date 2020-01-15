using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class RotaService : ServiceBase<Rota, RotaSummary, Guid>, IRotaService
    {
        private readonly IRotaRepository _RotaRepository;

        public RotaService(IRotaRepository RotaRepository)
        {
            _RotaRepository = RotaRepository;
        }

        public override string GetTag()
        {
            return "rota";
        }

        protected override Task<Rota> CreateEntryAsync(RotaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Rota = new Rota
            {
                Id = summary.Id
            };
            return Task.FromResult(Rota);
        }

        protected override Task<RotaSummary> CreateSummaryAsync(Rota entry)
        {
            var Rota = new RotaSummary
            {
                Id = entry.Id
            };

            return Task.FromResult(Rota);
        }

        protected override Guid GetKeyFromSummary(RotaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Rota> GetRepository()
        {
            return _RotaRepository;
        }

        protected override void UpdateEntry(Rota entry, RotaSummary summary)
        {
        }

        protected override void ValidateSummary(RotaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Rota: sumário é obrigatório"));
            }
        }
    }
}
