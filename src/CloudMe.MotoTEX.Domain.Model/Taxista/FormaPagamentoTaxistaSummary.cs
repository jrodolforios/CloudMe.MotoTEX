using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Taxista
{
    public class FormaPagamentoTaxistaSummary
    {
        public Guid Id { get; set; }
        public Guid IdFormaPagamento { get; set; }
        public Guid IdTaxista { get; set; }
    }
}
