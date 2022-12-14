using System;
using System.Collections.Generic;
using CloudMe.MotoTEX.Domain.Enums;

namespace CloudMe.MotoTEX.Infraestructure.Entries
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

        public Guid? IdRota { get; set; }
        public virtual Rota Rota { get; set; }

        public Guid IdFormaPagamento { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }

        public Guid? IdFaixaDesconto { get; set; }
        public virtual FaixaDesconto FaixaDesconto { get; set; }

        public TipoAtendimento TipoAtendimento { get; set; }

        public DateTime? Data { get; set; }

        public int ETA { get; set; }
        public int? TempoDisponivel { get; set; }

        public float? ValorEstimado { get; set; }
        public float? ValorProposto { get; set; }

        public SituacaoSolicitacaoCorrida Situacao { get; set; }

        // Status do monitoramento da solicitação (reservado)
        public StatusMonitoramentoSolicitacaoCorrida StatusMonitoramento { get; set; }

        public virtual Corrida Corrida { get; set; }

        public virtual IEnumerable<SolicitacaoCorridaTaxista> Taxistas { get; set; } // taxistas que responderam à solicitação

        public bool IsInterUrbano { get; set; }

        // Faixas de busca correntes no monitoramento da solicitação (reservado)
        public int IdxFaixaBusca { get; set; }
    }
}
