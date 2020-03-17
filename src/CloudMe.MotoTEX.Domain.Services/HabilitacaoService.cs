using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Infraestructure.Entries.Piloto;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class HabilitacaoService : ServiceBase<Habilitacao, HabilitacaoSummary, Guid>, IHabilitacaoService
    {
        private readonly IHabilitacaoRepository _HabilitacaoRepository;

        public HabilitacaoService(IHabilitacaoRepository HabilitacaoRepository)
        {
            _HabilitacaoRepository = HabilitacaoRepository;
        }

        public override string GetTag()
        {
            return "Habilitacao";
        }

        protected override async Task<Habilitacao> CreateEntryAsync(HabilitacaoSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new Habilitacao
                {
                    Id = summary.Id,
                    IdTaxista = summary.IdTaxista,
                    IdFoto = summary.IdFoto,
                    IdUF = summary.IdUF,
                    Categoria = summary.Categoria,
                    Validade = summary.Validade,
                    PrimeiraHabilitacao = summary.PrimeiraHabilitacao
                };
            });
        }

        protected override async Task<HabilitacaoSummary> CreateSummaryAsync(Habilitacao entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;
                return new HabilitacaoSummary
                {
                    Id = entry.Id,
                    IdTaxista = entry.IdTaxista,
                    IdFoto = entry.IdFoto,
                    IdUF = entry.IdUF,
                    Categoria = entry.Categoria,
                    Validade = entry.Validade,
                    PrimeiraHabilitacao = entry.PrimeiraHabilitacao
                };
            });
        }

        protected override Guid GetKeyFromSummary(HabilitacaoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Habilitacao> GetRepository()
        {
            return _HabilitacaoRepository;
        }

        protected override void UpdateEntry(Habilitacao entry, HabilitacaoSummary summary)
        {
            entry.IdTaxista = summary.IdTaxista;
            entry.IdFoto = summary.IdFoto;
            entry.IdUF = summary.IdUF;
            entry.Categoria = summary.Categoria;
            entry.Validade = summary.Validade;
            entry.PrimeiraHabilitacao = summary.PrimeiraHabilitacao;
        }

        protected override void ValidateSummary(HabilitacaoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Habilitacao: sumário é obrigatório"));
            }
        }
    }
}
