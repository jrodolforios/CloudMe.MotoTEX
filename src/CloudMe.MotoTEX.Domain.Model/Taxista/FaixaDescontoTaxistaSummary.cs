using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Taxista
{
    public class FaixaDescontoTaxistaSummary
    {
        public Guid Id { get; set; }
        public Guid IdFaixaDesconto { get; set; }
        public Guid IdTaxista { get; set; }
    }
}
