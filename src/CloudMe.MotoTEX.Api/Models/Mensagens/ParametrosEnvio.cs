using CloudMe.MotoTEX.Domain.Model.Mensagem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Api.Models.Mensagens
{
    public class ParametrosEnvio
    {
        public MensagemSummary mensagem { get; set; }
        public DestinatariosMensagem destinatarios { get; set; }
    }
}
