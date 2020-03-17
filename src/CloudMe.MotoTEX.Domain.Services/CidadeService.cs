using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Infraestructure.Entries.Localidade;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class CidadeService : ServiceBase<Cidade, CidadeSummary, Guid>, ICidadeService
    {
        private readonly ICidadeRepository _CidadeRepository;

        public CidadeService(ICidadeRepository CidadeRepository)
        {
            _CidadeRepository = CidadeRepository;
        }

        public override string GetTag()
        {
            return "Cidade";
        }

        protected override async Task<Cidade> CreateEntryAsync(CidadeSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new Cidade
                {
                    Id = summary.Id,
                    Nome = summary.Nome,
                    IdUF = summary.IdUF
                };
            });
        }

        protected override async Task<CidadeSummary> CreateSummaryAsync(Cidade entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;
                return new CidadeSummary
                {
                    Id = entry.Id,
                    Nome = entry.Nome,
                    IdUF = entry.IdUF
                };
            });
        }

        protected override Guid GetKeyFromSummary(CidadeSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Cidade> GetRepository()
        {
            return _CidadeRepository;
        }

        protected override void UpdateEntry(Cidade entry, CidadeSummary summary)
        {
            entry.Nome = summary.Nome;
            entry.IdUF = summary.IdUF;
        }

        protected override void ValidateSummary(CidadeSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Cidade: sumário é obrigatório"));
            }
        }
    }
}
