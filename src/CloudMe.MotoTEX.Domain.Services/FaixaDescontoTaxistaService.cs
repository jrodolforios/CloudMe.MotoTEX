﻿using prmToolkit.NotificationPattern;
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
    public class FaixaDescontoTaxistaService : ServiceBase<FaixaDescontoTaxista, FaixaDescontoTaxistaSummary, Guid>, IFaixaDescontoTaxistaService
    {
        private readonly IFaixaDescontoTaxistaRepository _FaixaDescontoTaxistaRepository;

        public FaixaDescontoTaxistaService(IFaixaDescontoTaxistaRepository FaixaDescontoTaxistaRepository)
        {
            _FaixaDescontoTaxistaRepository = FaixaDescontoTaxistaRepository;
        }

        public override string GetTag()
        {
            return "faixa_desconto_taxista";
        }

		public Task<bool> DeleteByTaxistId(Guid id)
        {
            var list = _FaixaDescontoTaxistaRepository.FindAll().Where(x => x.IdTaxista == id);

            list.ToList().ForEach(async x =>
            {
                await _FaixaDescontoTaxistaRepository.DeleteAsync(x, false);
            });

            return Task.FromResult(true);
        }

        public Task<List<FaixaDescontoTaxistaSummary>> GetByTaxistId(Guid id)
        {
            var list = _FaixaDescontoTaxistaRepository.FindAll().Where(x => x.IdTaxista == id);

            var listaRetorno = new List<FaixaDescontoTaxistaSummary>();

            list.ToList().ForEach(async x =>
            {
                var summary = await CreateSummaryAsync(x);
                listaRetorno.Add(summary);
            });

            return Task.FromResult(listaRetorno);
        }
        
        protected override Task<FaixaDescontoTaxista> CreateEntryAsync(FaixaDescontoTaxistaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var FaixaDescontoTaxista = new FaixaDescontoTaxista
            {
                Id = summary.Id,
                IdFaixaDesconto = summary.IdFaixaDesconto,
                IdTaxista = summary.IdTaxista
            };
            return Task.FromResult(FaixaDescontoTaxista);
        }

        protected override Task<FaixaDescontoTaxistaSummary> CreateSummaryAsync(FaixaDescontoTaxista entry)
        {
            var FaixaDescontoTaxista = new FaixaDescontoTaxistaSummary
            {
                Id = entry.Id,
                IdFaixaDesconto = entry.IdFaixaDesconto,
                IdTaxista = entry.IdTaxista
            };

            return Task.FromResult(FaixaDescontoTaxista);
        }

        protected override Guid GetKeyFromSummary(FaixaDescontoTaxistaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<FaixaDescontoTaxista> GetRepository()
        {
            return _FaixaDescontoTaxistaRepository;
        }

        protected override void UpdateEntry(FaixaDescontoTaxista entry, FaixaDescontoTaxistaSummary summary)
        {
            entry.IdFaixaDesconto = summary.IdFaixaDesconto;
            entry.IdTaxista = summary.IdTaxista;
        }

        protected override void ValidateSummary(FaixaDescontoTaxistaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "FaixaDescontoTaxista: sumário é obrigatório"));
            }

            if (summary.IdFaixaDesconto.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdFaixaDesconto", "FaixaDescontoTaxista: faixa de desconto inexistente ou não informada"));
            }

            if (summary.IdTaxista.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdTaxista", "FaixaDescontoTaxista: taxista inexistente ou não informado"));
            }
        }
    }
}