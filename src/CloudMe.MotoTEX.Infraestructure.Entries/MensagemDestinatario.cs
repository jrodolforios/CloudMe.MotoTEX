using CloudMe.MotoTEX.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Entries
{
    public class MensagemDestinatario : EntryBase<Guid>
    {
        public Guid IdMensagem { get; set; }
        public virtual Mensagem Mensagem { get; set; }

        public Guid IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public Guid? IdGrupoUsuario { get; set; } // nulo se particular/privado
        public virtual GrupoUsuario GrupoUsuarios { get; set; }

        public DateTime? DataRecebimento { get; set; }
        public DateTime? DataLeitura { get; set; }

        public StatusMensagem Status { get; set; }
    }
}
