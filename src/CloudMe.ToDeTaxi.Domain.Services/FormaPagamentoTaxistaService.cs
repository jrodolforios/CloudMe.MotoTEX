using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
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

        protected override Task<FormaPagamentoTaxista> CreateEntryAsync(FormaPagamentoTaxistaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var FormaPagamentoTaxista = new FormaPagamentoTaxista
            {
                Id = summary.Id,
                IdFormaPagamento = summary.IdFormaPagamento,
                IdTaxista = summary.IdTaxista
            };
            return Task.FromResult(FormaPagamentoTaxista);
        }

        protected override Task<FormaPagamentoTaxistaSummary> CreateSummaryAsync(FormaPagamentoTaxista entry)
        {
            var FormaPagamentoTaxista = new FormaPagamentoTaxistaSummary
            {
                Id = entry.Id,
                IdFormaPagamento = entry.IdFormaPagamento,
                IdTaxista = entry.IdTaxista
            };

            return Task.FromResult(FormaPagamentoTaxista);
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
