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
    public class FaixaDescontoService : ServiceBase<FaixaDesconto, FaixaDescontoSummary, Guid>, IFaixaDescontoService
    {
        private readonly IFaixaDescontoRepository _FaixaDescontoRepository;

        public FaixaDescontoService(IFaixaDescontoRepository FaixaDescontoRepository)
        {
            _FaixaDescontoRepository = FaixaDescontoRepository;
        }

        public override string GetTag()
        {
            return "faixa_desconto";
        }

        protected override Task<FaixaDesconto> CreateEntryAsync(FaixaDescontoSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var FaixaDesconto = new FaixaDesconto
            {
                Id = summary.Id,
                Valor = summary.Valor,
                Descricao = summary.Descricao
            };
            return Task.FromResult(FaixaDesconto);
        }

        protected override Task<FaixaDescontoSummary> CreateSummaryAsync(FaixaDesconto entry)
        {
            var FaixaDesconto = new FaixaDescontoSummary
            {
                Id = entry.Id,
                Valor = entry.Valor,
                Descricao = entry.Descricao
            };

            return Task.FromResult(FaixaDesconto);
        }

        protected override Guid GetKeyFromSummary(FaixaDescontoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<FaixaDesconto> GetRepository()
        {
            return _FaixaDescontoRepository;
        }

        protected override void UpdateEntry(FaixaDesconto entry, FaixaDescontoSummary summary)
        {
            entry.Valor = summary.Valor;
            entry.Descricao = summary.Descricao;
        }

        protected override void ValidateSummary(FaixaDescontoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "FaixaDesconto: sumário é obrigatório"));
            }

            if (summary.Valor < 0)
            {
                this.AddNotification(new Notification("Valor", "FaixaDesconto: valor não é válido"));
            }

            if (summary.Valor > 100.0)
            {
                this.AddNotification(new Notification("Valor", "FaixaDesconto: valor deve estar na faixa entre 0% e 100%"));
            }
        }
    }
}
