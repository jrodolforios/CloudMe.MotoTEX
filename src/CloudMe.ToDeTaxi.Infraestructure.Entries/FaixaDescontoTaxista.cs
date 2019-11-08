using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class FaixaDescontoTaxista : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public Guid IdFaixaDesconto { get; set; }
        public virtual FaixaDesconto FaixaDesconto { get; set; }

        public Guid IdTaxista { get; set; }
        public virtual Taxista Taxista { get; set; }
    }
}
