using System;
using System.Collections.Generic;

namespace CloudMe.MotoTEX.Infraestructure.Entries
{
    public class PontoTaxi : EntryBase<Guid>
    {
        //public Guid Id { get; set; }
        public string Nome { get; set; }

        public Guid IdEndereco { get; set; }
        public virtual Endereco Endereco { get; set; }

        public virtual IEnumerable<Taxista> Taxistas { get; set; }
    }
}
