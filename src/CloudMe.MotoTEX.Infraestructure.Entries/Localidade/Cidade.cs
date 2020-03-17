using System;

namespace CloudMe.MotoTEX.Infraestructure.Entries.Localidade
{
    public class Cidade : EntryBase<Guid>
    {
        public string Nome { get; set; }
        public Guid IdUF { get; set; }
        public virtual UF UF { get; set; }
    }
}
