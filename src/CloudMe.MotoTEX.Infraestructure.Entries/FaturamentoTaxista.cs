using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Entries
{
    public class FaturamentoTaxista : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public Guid IdFaturamento { get; set; }
        public virtual Faturamento Faturamento { get; set; }

        public Guid IdTaxista { get; set; }
        public virtual Taxista Taxista { get; set; }

        public decimal Total { get; set; }

        public string LinkBoleto { get; set; }

        public string JsonBoletoAPI { get; set; }

        public DateTime DataGeracao { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime DataPagamento { get; set; }

        public virtual IEnumerable<Corrida> Corrida { get; set; }

    }
}
