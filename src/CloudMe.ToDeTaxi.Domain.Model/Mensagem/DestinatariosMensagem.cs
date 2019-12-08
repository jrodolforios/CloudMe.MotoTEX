using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Mensagem
{
    public class DestinatariosMensagem
    {
        public IEnumerable<Guid> IdsUsuarios { get; set; }
        public IEnumerable<Guid> IdsGruposUsuarios { get; set; }
    }
}
