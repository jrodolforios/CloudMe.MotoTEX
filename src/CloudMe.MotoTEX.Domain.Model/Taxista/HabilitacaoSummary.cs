using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Taxista
{
    public class HabilitacaoSummary
    {
        public Guid Id { get; set; }
        public Guid IdTaxista { get; set; }
        public Guid IdFoto { get; set; }
        public Guid IdUF { get; set; }
        public string Categoria { get; set; }
        public DateTime Validade { get; set; }
        public DateTime PrimeiraHabilitacao { get; set; }
    }
}
