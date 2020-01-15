using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class FormaPagamentoService : ServiceBase<FormaPagamento, FormaPagamentoSummary, Guid>, IFormaPagamentoService
    {
        private readonly IFormaPagamentoRepository _FormaPagamentoRepository;

        public FormaPagamentoService(IFormaPagamentoRepository FormaPagamentoRepository)
        {
            _FormaPagamentoRepository = FormaPagamentoRepository;
        }

        public override string GetTag()
        {
            return "forma_pagamento";
        }

        protected override Task<FormaPagamento> CreateEntryAsync(FormaPagamentoSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var FormaPagamento = new FormaPagamento
            {
                Id = summary.Id,
                Descricao = summary.Descricao
            };
            return Task.FromResult(FormaPagamento);
        }

        protected override Task<FormaPagamentoSummary> CreateSummaryAsync(FormaPagamento entry)
        {
            var FormaPagamento = new FormaPagamentoSummary
            {
                Id = entry.Id,
                Descricao = entry.Descricao
            };

            return Task.FromResult(FormaPagamento);
        }

        protected override Guid GetKeyFromSummary(FormaPagamentoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<FormaPagamento> GetRepository()
        {
            return _FormaPagamentoRepository;
        }

        protected override void UpdateEntry(FormaPagamento entry, FormaPagamentoSummary summary)
        {
            entry.Descricao = summary.Descricao;
        }

        protected override void ValidateSummary(FormaPagamentoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "FormaPagamento: sumário é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.Descricao))
            {
                this.AddNotification(new Notification("Descricao", "FormaPagamento: descrição é obrigatória"));
            }
        }
    }
}
