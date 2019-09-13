using System;
using System.Collections.Generic;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class PontoTaxi : EntryBase
    {
        public Guid Id { get; set; }
        public String Nome { get; set; }

        public Guid IdEndereco { get; set; }
        public virtual Localizacao Endereco { get; set; }

        public virtual IEnumerable<Taxista> Taxistas { get; set; }
    }
}
