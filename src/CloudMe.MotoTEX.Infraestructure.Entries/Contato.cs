using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Entries
{
    public class Contato : EntryBase<Guid>
    {
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }

        public Guid? IdTaxista { get; set; }
        public virtual Taxista Taxista { get; set; }

        public Guid? IdPassageiro { get; set; }
        public virtual Passageiro Passageiro { get; set; }
    }
}
