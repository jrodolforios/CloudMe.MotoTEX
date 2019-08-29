﻿using prmToolkit.NotificationPattern;
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
    public class SolicitacaoCorridaService : ServiceBase<SolicitacaoCorrida, SolicitacaoCorridaSummary, Guid>, ISolicitacaoCorridaService
    {
        private readonly ISolicitacaoCorridaRepository _SolicitacaoCorridaRepository;

        public SolicitacaoCorridaService(ISolicitacaoCorridaRepository SolicitacaoCorridaRepository)
        {
            _SolicitacaoCorridaRepository = SolicitacaoCorridaRepository;
        }

        protected override Task<SolicitacaoCorrida> CreateEntryAsync(SolicitacaoCorridaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var SolicitacaoCorrida = new SolicitacaoCorrida
            {
                Id = summary.Id,
                IdPassageiro = summary.IdPassageiro,
                IdLocalizacaoOrigem = summary.IdLocalizacaoOrigem,
                IdLocalizacaoDestino = summary.IdLocalizacaoDestino,
                IdRota = summary.IdRota,
                IdFaixaDesconto = summary.IdFaixaDesconto,
                IdFormaPagamento = summary.IdFormaPagamento,
                TipoAtendimento = summary.TipoAtendimento,
                Data = summary.Data,
                ETA = summary.ETA,
                TempoDisponivel = summary.TempoDisponivel,
                ValorEstimado = summary.ValorEstimado,
                ValorProposto = summary.ValorProposto,
                Situacao = summary.Situacao
            };
            return Task.FromResult(SolicitacaoCorrida);
        }

        protected override Task<SolicitacaoCorridaSummary> CreateSummaryAsync(SolicitacaoCorrida entry)
        {
            var SolicitacaoCorrida = new SolicitacaoCorridaSummary
            {
                Id = entry.Id,
                IdPassageiro = entry.IdPassageiro,
                IdLocalizacaoOrigem = entry.IdLocalizacaoOrigem,
                IdLocalizacaoDestino = entry.IdLocalizacaoDestino,
                IdRota = entry.IdRota,
                IdFaixaDesconto = entry.IdFaixaDesconto,
                IdFormaPagamento = entry.IdFormaPagamento,
                TipoAtendimento = entry.TipoAtendimento,
                Data = entry.Data,
                ETA = entry.ETA,
                TempoDisponivel = entry.TempoDisponivel,
                ValorEstimado = entry.ValorEstimado,
                ValorProposto = entry.ValorProposto,
                Situacao = entry.Situacao
            };

            return Task.FromResult(SolicitacaoCorrida);
        }

        protected override Guid GetKeyFromSummary(SolicitacaoCorridaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<SolicitacaoCorrida> GetRepository()
        {
            return _SolicitacaoCorridaRepository;
        }

        protected override void UpdateEntry(SolicitacaoCorrida entry, SolicitacaoCorridaSummary summary)
        {
            entry.IdPassageiro = summary.IdPassageiro;
            entry.IdLocalizacaoOrigem = summary.IdLocalizacaoOrigem;
            entry.IdLocalizacaoDestino = summary.IdLocalizacaoDestino;
            entry.IdRota = summary.IdRota;
            entry.IdFaixaDesconto = summary.IdFaixaDesconto;
            entry.IdFormaPagamento = summary.IdFormaPagamento;
            entry.TipoAtendimento = summary.TipoAtendimento;
            entry.Data = summary.Data;
            entry.ETA = summary.ETA;
            entry.TempoDisponivel = summary.TempoDisponivel;
            entry.ValorEstimado = summary.ValorEstimado;
            entry.ValorProposto = summary.ValorProposto;
            entry.Situacao = summary.Situacao;
        }

        protected override void ValidateSummary(SolicitacaoCorridaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "SolicitacaoCorrida: summário é obrigatório"));
            }

            if (summary.IdPassageiro.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdPassageiro", "Passageiro: passasgeiro inexistente ou não informado"));
            }

            if (summary.IdLocalizacaoOrigem.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdLocalizacaoOrigem", "Passageiro: local de origem inexistente ou não informado"));
            }

            if (summary.IdLocalizacaoDestino.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdLocalizacaoDestino", "Passageiro: local de destino inexistente ou não informado"));
            }

            if (summary.IdRota.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdRota", "Passageiro: rota inexistente ou não informado"));
            }

            if (summary.IdFormaPagamento.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdFormaPagamento", "Passageiro: forma de pagamento inexistente ou não informado"));
            }

            if (summary.TipoAtendimento == Enums.TipoAtendimento.Indefinido)
            {
                this.AddNotification(new Notification("TipoAtendimento", "Passageiro: tipo de atendimento não definido"));
            }

            if (summary.Situacao == Enums.SituacaoSolicitacaoCorrida.Indefinido)
            {
                this.AddNotification(new Notification("Situacao", "Passageiro: situação não definida"));
            }
        }
    }
}