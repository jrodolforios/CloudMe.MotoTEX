using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class FormaPagamentoService : ServiceBase<FormaPagamento, FormaPagamentoSummary, Guid>, IFormaPagamentoService
    {
        private readonly IFormaPagamentoRepository _FormaPagamentoRepository;

        public FormaPagamentoService(IFormaPagamentoRepository FormaPagamentoRepository)
        {
            _FormaPagamentoRepository = FormaPagamentoRepository;
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
