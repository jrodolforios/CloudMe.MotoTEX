using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Favorito : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public Guid IdPassageiro { get; set; }
        public virtual Passageiro Passageiro { get; set; }

        public Guid IdTaxista { get; set; }
        public virtual Taxista Taxista { get; set; }

        public int Preferencia { get; set; }
    }
}
