using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Entries
{
    public class FormaPagamentoTaxista : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public Guid IdFormaPagamento { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }

        public Guid IdTaxista { get; set; }
        public virtual Taxista Taxista { get; set; }
    }
}
