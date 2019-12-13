using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Mensagem : EntryBase<Guid>
    {
        public Guid IdRemetente { get; set; }
        public virtual Usuario Remetente { get; set; }

        public string Assunto { get; set; }
        public string Corpo { get; set; }

        public bool Apagada { get; set; } = false; // apagada pelo remetente

        public virtual IEnumerable<MensagemDestinatario> Destinatarios { get; set; }
    }
}
