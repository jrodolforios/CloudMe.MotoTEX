using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Entries.Localidade
{
    public class UF : EntryBase<Guid>
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public virtual IEnumerable<Cidade> Cidades { get; set; }
    }
}
