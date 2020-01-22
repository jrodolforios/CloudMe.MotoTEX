using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Entries
{
    public class Faturamento : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public int Ano { get; set; }

        public int Mes { get; set; }

        public decimal Total { get; set; }

        public decimal PercentualComissao { get; set; }

        public DateTime DataGeracao { get; set; }

        public virtual IEnumerable<FaturamentoTaxista> FaturamentoTaxista { get; set; }

    }
}
