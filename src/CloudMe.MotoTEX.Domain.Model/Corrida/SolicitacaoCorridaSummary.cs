using System;
using CloudMe.MotoTEX.Domain.Enums;

namespace CloudMe.MotoTEX.Domain.Model.Corrida
{
    public class SolicitacaoCorridaSummary
    {
        public Guid Id { get; set; }
        public Guid IdPassageiro { get; set; }
        public Guid IdLocalizacaoOrigem { get; set; }
        public Guid IdLocalizacaoDestino { get; set; }
        public Guid? IdRota { get; set; }
        public Guid IdFormaPagamento { get; set; }
        public Guid? IdFaixaDesconto { get; set; }
        public TipoAtendimento TipoAtendimento { get; set; }
        public DateTime? Data { get; set; }
        public int ETA { get; set; }
        public int? TempoDisponivel { get; set; }
        public float? ValorEstimado { get; set; }
        public float? ValorProposto { get; set; }
        public bool IsInterUrbano { get; set; }
        public SituacaoSolicitacaoCorrida Situacao { get; set; }
        public StatusMonitoramentoSolicitacaoCorrida StatusMonitoramento { get; set; }
        public int IdxFaixaBusca { get; set; }
        public string latitudeOrigem { get; set; }
        public string longitudeOrigem { get; set; }
        public string latitudeDestino { get; set; }
        public string longitudeDestino { get; set; }
    }
}
