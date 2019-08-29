using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class FormaPagamento : EntryBase
    {
        public Guid Id { get; set; }

        public string Descricao { get; set; }

        public virtual IEnumerable<FormaPagamentoTaxista> Taxistas { get; set; }
    }
}
