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
    public class CorridaService : ServiceBase<Corrida, CorridaSummary, Guid>, ICorridaService
    {
        private readonly ICorridaRepository _CorridaRepository;

        public CorridaService(ICorridaRepository CorridaRepository)
        {
            _CorridaRepository = CorridaRepository;
        }

        protected override Task<Corrida> CreateEntryAsync(CorridaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Corrida = new Corrida
            {
                Id = summary.Id,
                IdSolicitacao = summary.IdSolicitacao,
                IdTaxista = summary.IdTaxista,
                IdVeiculo = summary.IdVeiculo,
                IdRotaExecutada = summary.IdRotaExecutada,
                IdTarifa = summary.IdTarifa,
                Inicio = summary.Inicio,
                Fim = summary.Fim,
                AvaliacaoPassageiro = summary.AvaliacaoPassageiro,
                AvaliacaoTaxista = summary.AvaliacaoTaxista,
                Status = summary.Status,
                TempoEmEspera = summary.TempoEmEspera
            };
            return Task.FromResult(Corrida);
        }

        protected override Task<CorridaSummary> CreateSummaryAsync(Corrida entry)
        {
            var Corrida = new CorridaSummary
            {
                Id = entry.Id,
                IdSolicitacao = entry.IdSolicitacao,
                IdTaxista = entry.IdTaxista,
                IdVeiculo = entry.IdVeiculo,
                IdRotaExecutada = entry.IdRotaExecutada,
                IdTarifa = entry.IdTarifa,
                Inicio = entry.Inicio,
                Fim = entry.Fim,
                AvaliacaoPassageiro = entry.AvaliacaoPassageiro,
                AvaliacaoTaxista = entry.AvaliacaoTaxista,
                Status = entry.Status,
                TempoEmEspera = entry.TempoEmEspera
            };

            return Task.FromResult(Corrida);
        }

        protected override Guid GetKeyFromSummary(CorridaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Corrida> GetRepository()
        {
            return _CorridaRepository;
        }

        protected override void UpdateEntry(Corrida entry, CorridaSummary summary)
        {
            entry.IdSolicitacao = summary.IdSolicitacao;
            entry.IdTaxista = summary.IdTaxista;
            entry.IdVeiculo = summary.IdVeiculo;
            entry.IdRotaExecutada = summary.IdRotaExecutada;
            entry.IdTarifa = summary.IdTarifa;
            entry.Inicio = summary.Inicio;
            entry.Fim = summary.Fim;
            entry.AvaliacaoPassageiro = summary.AvaliacaoPassageiro;
            entry.AvaliacaoTaxista = summary.AvaliacaoTaxista;
            entry.Status = summary.Status;
            entry.TempoEmEspera = summary.TempoEmEspera;
        }

        protected override void ValidateSummary(CorridaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Corrida: sumário é obrigatório"));
            }

            if(summary.IdSolicitacao.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdSolicitacao", "Corrida: solicitação da corrida inexistente ou não informada"));
            }
        }
    }
}
