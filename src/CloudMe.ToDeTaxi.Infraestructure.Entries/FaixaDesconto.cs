using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class FaixaDesconto : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public float Valor { get; set; }

        public string Descricao { get; set; }

        public virtual IEnumerable<FaixaDescontoTaxista> Taxistas { get; set; }
    }
}
