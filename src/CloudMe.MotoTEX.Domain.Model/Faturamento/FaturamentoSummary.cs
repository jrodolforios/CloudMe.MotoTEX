using System;
using CloudMe.MotoTEX.Domain.Enums;

namespace CloudMe.MotoTEX.Domain.Model.Faturamento
{
    public class FaturamentoSummary
    {
        public Guid Id { get; set; }

        public int Ano { get; set; }

        public int Mes { get; set; }

        public decimal Total { get; set; }

        public decimal PercentualComissao { get; set; }

        public DateTime DataGeracao { get; set; }
    }
}
