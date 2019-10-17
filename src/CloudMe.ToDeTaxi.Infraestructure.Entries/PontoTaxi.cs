using System;
using System.Collections.Generic;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class PontoTaxi : EntryBase
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public Guid IdEndereco { get; set; }
        public virtual Endereco Endereco { get; set; }

        public virtual IEnumerable<Taxista> Taxistas { get; set; }
    }
}
