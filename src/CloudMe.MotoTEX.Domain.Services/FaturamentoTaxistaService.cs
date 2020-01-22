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
    public class FaturamentoTaxistaService : ServiceBase<FaturamentoTaxista, FaturamentoTaxistaSummary, Guid>, IFaturamentoTaxistaService
    {
        private readonly IFaturamentoTaxistaRepository _faturamentoTaxistaRepository;

        public FaturamentoTaxistaService(IFaturamentoTaxistaRepository faturamentoTaxistaRepository)
        {
            _faturamentoTaxistaRepository = faturamentoTaxistaRepository;
        }

        public override string GetTag()
        {
            return "faturamento";
        }
        
        protected override Task<FaturamentoTaxista> CreateEntryAsync(FaturamentoTaxistaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var faturamento = new FaturamentoTaxista
            {
                Id = summary.Id,
                DataGeracao = summary.DataGeracao,
                DataPagamento = summary.DataPagamento,
                DataVencimento = summary.DataVencimento,
                IdFaturamento = summary.IdFaturamento,
                IdTaxista = summary.IdTaxista,
                JsonBoletoAPI = summary.JsonBoletoAPI,
                LinkBoleto = summary.LinkBoleto,
                Total = summary.Total
            };
            return Task.FromResult(faturamento);
        }

        protected override Task<FaturamentoTaxistaSummary> CreateSummaryAsync(FaturamentoTaxista entry)
        {
            var faturamento = new FaturamentoTaxistaSummary
            {
                Id = entry.Id,
                DataGeracao = entry.DataGeracao,
                DataPagamento = entry.DataPagamento,
                DataVencimento = entry.DataVencimento,
                IdFaturamento = entry.IdFaturamento,
                IdTaxista = entry.IdTaxista,
                JsonBoletoAPI = entry.JsonBoletoAPI,
                Total = entry.Total,
                LinkBoleto = entry.LinkBoleto
            };

            return Task.FromResult(faturamento);
        }

        protected override Guid GetKeyFromSummary(FaturamentoTaxistaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<FaturamentoTaxista> GetRepository()
        {
            return _faturamentoTaxistaRepository;
        }

        protected override void UpdateEntry(FaturamentoTaxista entry, FaturamentoTaxistaSummary summary)
        {
            entry.DataGeracao = summary.DataGeracao;
            entry.DataPagamento = summary.DataPagamento;
            entry.DataVencimento = summary.DataVencimento;
            entry.Id = summary.Id;
            entry.IdFaturamento = summary.IdFaturamento;
            entry.IdTaxista = summary.IdTaxista;
            entry.JsonBoletoAPI = summary.JsonBoletoAPI;
            entry.LinkBoleto = summary.LinkBoleto;
            entry.Total = summary.Total;
        }

        protected override void ValidateSummary(FaturamentoTaxistaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Faturamento: sumário é obrigatório"));
            }

        }
    }
}
