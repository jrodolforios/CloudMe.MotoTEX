using CloudMe.MotoTEX.Infraestructure.Entries.Localidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Entries
{
    public class Associacao : EntryBase<Guid>
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public string IdCidade { get; set; }
        public string IdFotoLOGO { get; set; }

        public virtual Cidade Cidade { get; set; }
        public virtual Foto Logo { get; set; }
    }
}
