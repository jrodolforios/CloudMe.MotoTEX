using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Model.Corrida;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class FaixaAtivacaoService : ServiceBase<FaixaAtivacao, FaixaAtivacaoSummary, Guid>, IFaixaAtivacaoService
    {
        private readonly IFaixaAtivacaoRepository _FaixaAtivacaoRepository;

        public FaixaAtivacaoService(IFaixaAtivacaoRepository FaixaAtivacaoRepository)
        {
            _FaixaAtivacaoRepository = FaixaAtivacaoRepository;
        }

        public override string GetTag()
        {
            return "FaixaAtivacao";
        }

        protected override async Task<FaixaAtivacao> CreateEntryAsync(FaixaAtivacaoSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new FaixaAtivacao
                {
                    Id = summary.Id,
                    Raio = summary.Raio,
                    Janela = summary.Janela
                };
            });
        }

        protected override async Task<FaixaAtivacaoSummary> CreateSummaryAsync(FaixaAtivacao entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;
                return new FaixaAtivacaoSummary
                {
                    Id = entry.Id,
                    Raio = entry.Raio,
                    Janela = entry.Janela                    
                };
            });
        }

        protected override Guid GetKeyFromSummary(FaixaAtivacaoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<FaixaAtivacao> GetRepository()
        {
            return _FaixaAtivacaoRepository;
        }

        protected override void UpdateEntry(FaixaAtivacao entry, FaixaAtivacaoSummary summary)
        {
            entry.Raio = summary.Raio;
            entry.Janela = summary.Janela;
        }

        protected override void ValidateSummary(FaixaAtivacaoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "FaixaAtivacao: sumário é obrigatório"));
            }
        }

        public async Task<IEnumerable<FaixaAtivacao>> GetAllByRadius()
        {
            return await _FaixaAtivacaoRepository.GetAllByRadius();
        }

    }
}
