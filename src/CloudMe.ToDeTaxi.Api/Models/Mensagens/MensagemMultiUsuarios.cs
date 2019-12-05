using CloudMe.ToDeTaxi.Domain.Model.Mensagem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Api.Models.Mensagens
{
    public class MensagemMultiUsuarios
    {
        public MensagemSummary mensagem { get; set; }
        public IEnumerable<Guid> ids_usuarios { get; set; }
    }
}
