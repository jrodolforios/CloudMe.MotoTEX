using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Taxista
{
    public class FormaPagamentoTaxistaSummary
    {
        public Guid Id { get; set; }
        public Guid IdFormaPagamento { get; set; }
        public Guid IdTaxista { get; set; }
    }
}
