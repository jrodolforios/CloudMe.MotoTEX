using System;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class SolicitacaoCorrida : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public Guid IdPassageiro { get; set; }
        public virtual Passageiro Passageiro { get; set; }

        public Guid IdLocalizacaoOrigem { get; set; }
        public virtual Localizacao LocalizacaoOrigem { get; set; }

        public Guid IdLocalizacaoDestino { get; set; }
        public virtual Localizacao LocalizacaoDestino { get; set; }

        public Guid IdRota { get; set; }
        public virtual Rota Rota { get; set; }

        public Guid IdFormaPagamento { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }

        public Guid? IdFaixaDesconto { get; set; }
        public virtual FaixaDesconto FaixaDesconto { get; set; }

        public TipoAtendimento TipoAtendimento { get; set; }

        public DateTime Data { get; set; }

        public int ETA { get; set; }
        public int? TempoDisponivel { get; set; }

        public float? ValorEstimado { get; set; }
        public float? ValorProposto { get; set; }

        public SituacaoSolicitacaoCorrida Situacao { get; set; }

        public virtual Corrida Corrida { get; set; }
    }
}
