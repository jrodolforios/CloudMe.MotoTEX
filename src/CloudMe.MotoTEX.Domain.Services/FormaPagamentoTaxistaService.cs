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

namespace CloudMe.MotoTEX.Domain.Services
{
    public class FormaPagamentoTaxistaService : ServiceBase<FormaPagamentoTaxista, FormaPagamentoTaxistaSummary, Guid>, IFormaPagamentoTaxistaService
    {
        private readonly IFormaPagamentoTaxistaRepository _FormaPagamentoTaxistaRepository;

        public FormaPagamentoTaxistaService(IFormaPagamentoTaxistaRepository FormaPagamentoTaxistaRepository)
        {
            _FormaPagamentoTaxistaRepository = FormaPagamentoTaxistaRepository;
        }
		
		public override string GetTag()
        {
            return "forma_pagamento_taxista";
        }
        
        public async Task<bool> DeleteByTaxistId(Guid id)
        {
            var list = await _FormaPagamentoTaxistaRepository.Search(x => x.IdTaxista == id);

            list.ToList().ForEach(async x =>
            {
                await _FormaPagamentoTaxistaRepository.DeleteAsync(x, false);
            });

            return true;
        }

        public async Task<IEnumerable<FormaPagamentoTaxistaSummary>> GetByTaxistId(Guid id)
        {
            var list = await _FormaPagamentoTaxistaRepository.Search(x => x.IdTaxista == id);
            return await GetAllSummariesAsync(list);
        }

        protected override async Task<FormaPagamentoTaxista> CreateEntryAsync(FormaPagamentoTaxistaSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new FormaPagamentoTaxista
                {
                    Id = summary.Id,
                    IdFormaPagamento = summary.IdFormaPagamento,
                    IdTaxista = summary.IdTaxista
                };
            });
        }

        protected override async Task<FormaPagamentoTaxistaSummary> CreateSummaryAsync(FormaPagamentoTaxista entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;

                return new FormaPagamentoTaxistaSummary
                {
                    Id = entry.Id,
                    IdFormaPagamento = entry.IdFormaPagamento,
                    IdTaxista = entry.IdTaxista
                };
            });
        }

        protected override Guid GetKeyFromSummary(FormaPagamentoTaxistaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<FormaPagamentoTaxista> GetRepository()
        {
            return _FormaPagamentoTaxistaRepository;
        }

        protected override void UpdateEntry(FormaPagamentoTaxista entry, FormaPagamentoTaxistaSummary summary)
        {
            entry.IdFormaPagamento = summary.IdFormaPagamento;
            entry.IdTaxista = summary.IdTaxista;
        }

        protected override void ValidateSummary(FormaPagamentoTaxistaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "FormaPagamentoTaxista: sumário é obrigatório"));
            }

            if (summary.IdFormaPagamento.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdFormaPagamento", "FormaPagamentoTaxista: forma de pagamento inexistente ou não informada"));
            }

            if (summary.IdTaxista.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdTaxista", "FormaPagamentoTaxista: taxista inexistente ou não informado"));
            }
        }
    }
}
