using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Infraestructure.Entries.Localidade;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class UFService : ServiceBase<UF, UFSummary, Guid>, IUFService
    {
        private readonly IUFRepository _UFRepository;

        public UFService(IUFRepository UFRepository)
        {
            _UFRepository = UFRepository;
        }

        public override string GetTag()
        {
            return "UF";
        }

        protected override async Task<UF> CreateEntryAsync(UFSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new UF
                {
                    Id = summary.Id,
                    Nome = summary.Nome,
                    Sigla = summary.Sigla
                };
            });
        }

        protected override async Task<UFSummary> CreateSummaryAsync(UF entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;
                return new UFSummary
                {
                    Id = entry.Id,
                    Nome = entry.Nome,
                    Sigla = entry.Sigla
                };
            });
        }

        protected override Guid GetKeyFromSummary(UFSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<UF> GetRepository()
        {
            return _UFRepository;
        }

        protected override void UpdateEntry(UF entry, UFSummary summary)
        {
            entry.Nome = summary.Nome;
            entry.Sigla = summary.Sigla;
        }

        protected override void ValidateSummary(UFSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "UF: sumário é obrigatório"));
            }
        }
    }
}
