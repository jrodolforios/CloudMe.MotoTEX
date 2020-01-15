using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Entries
{
    public class FormaPagamento : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public string Descricao { get; set; }

        public virtual IEnumerable<FormaPagamentoTaxista> Taxistas { get; set; }
    }
}
