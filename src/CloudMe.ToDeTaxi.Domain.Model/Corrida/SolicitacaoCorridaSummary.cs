using System;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Domain.Model.Corrida
{
    public class SolicitacaoCorridaSummary
    {
        public Guid Id { get; set; }
        public Guid IdPassageiro { get; set; }
        public Guid IdLocalizacaoOrigem { get; set; }
        public Guid IdLocalizacaoDestino { get; set; }
        public Guid IdRota { get; set; }
        public Guid IdFormaPagamento { get; set; }
        public Guid? IdFaixaDesconto { get; set; }
        public TipoAtendimento TipoAtendimento { get; set; }
        public DateTime Data { get; set; }
        public int ETA { get; set; }
        public int? TempoDisponivel { get; set; }
        public float? ValorEstimado { get; set; }
        public float? ValorProposto { get; set; }
        public SituacaoSolicitacaoCorrida Situacao { get; set; }
    }
}
