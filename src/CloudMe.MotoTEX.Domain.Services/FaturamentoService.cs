using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using CloudMe.MotoTEX.Domain.Model.Faturamento;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class FaturamentoService : ServiceBase<Faturamento, FaturamentoSummary, Guid>, IFaturamentoService
    {
        private readonly IFaturamentoRepository _faturamentoRepository;

        public FaturamentoService(IFaturamentoRepository faturamentoRepository)
        {
            _faturamentoRepository = faturamentoRepository;
        }

        public override string GetTag()
        {
            return "faturamento";
        }
        
        protected override Task<Faturamento> CreateEntryAsync(FaturamentoSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var faturamento = new Faturamento
            {
                Id = summary.Id,
                Ano = summary.Ano,
                Mes = summary.Mes,
                DataGeracao = summary.DataGeracao,
                PercentualComissao = summary.PercentualComissao,
                Total = summary.Total
            };
            return Task.FromResult(faturamento);
        }

        protected override Task<FaturamentoSummary> CreateSummaryAsync(Faturamento entry)
        {
            var faturamento = new FaturamentoSummary
            {
                Id = entry.Id,
                Ano = entry.Ano,
                Total = entry.Total,
                PercentualComissao = entry.PercentualComissao,
                DataGeracao = entry.DataGeracao,
                Mes = entry.Mes
            };

            return Task.FromResult(faturamento);
        }

        protected override Guid GetKeyFromSummary(FaturamentoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Faturamento> GetRepository()
        {
            return _faturamentoRepository;
        }

        protected override void UpdateEntry(Faturamento entry, FaturamentoSummary summary)
        {
            entry.Ano = summary.Ano;
            entry.DataGeracao = summary.DataGeracao;
            entry.Id = summary.Id;
            entry.Mes = summary.Mes;
            entry.PercentualComissao = summary.PercentualComissao;
            entry.Total = summary.Total;
        }

        protected override void ValidateSummary(FaturamentoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Faturamento: sumário é obrigatório"));
            }

        }
    }
}
