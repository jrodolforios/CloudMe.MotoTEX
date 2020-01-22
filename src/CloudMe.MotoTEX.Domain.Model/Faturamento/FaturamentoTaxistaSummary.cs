using System;
using CloudMe.MotoTEX.Domain.Enums;

namespace CloudMe.MotoTEX.Domain.Model.Faturamento
{
    public class FaturamentoTaxistaSummary
    {
        public Guid Id { get; set; }
        public Guid IdFaturamento { get; set; }

        public Guid IdTaxista { get; set; }

        public decimal Total { get; set; }

        public string LinkBoleto { get; set; }

        public string JsonBoletoAPI { get; set; }

        public DateTime DataGeracao { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime DataPagamento { get; set; }
    }
}
