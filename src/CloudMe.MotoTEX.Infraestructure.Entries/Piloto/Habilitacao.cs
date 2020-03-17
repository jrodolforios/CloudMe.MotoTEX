using CloudMe.MotoTEX.Infraestructure.Entries.Localidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Entries.Piloto
{
    public class Habilitacao: EntryBase<Guid>
    {
        public Guid IdTaxista { get; set; }
        public Guid IdFoto { get; set; }
        public Guid IdUF { get; set; }
        public string Categoria { get; set; }
        public DateTime Validade { get; set; }
        public DateTime PrimeiraHabilitacao { get; set; }

        public virtual Taxista Taxista { get; set; }
        public virtual Foto Foto { get; set; }
        public virtual UF UF { get; set; }
    }
}
