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

        protected override async Task<Rota> CreateEntryAsync(RotaSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new Rota
                {
                    Id = summary.Id
                };
            });
        }

        protected override async Task<RotaSummary> CreateSummaryAsync(Rota entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;
                return new RotaSummary
                {
                    Id = entry.Id
                };
            });
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
