using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Mensagem
{
    public class DetalhesMensagem
    {
        public Guid IdMensagem { get; set; }
        public Guid IdRemetente { get; set; }
        public DestinatariosMensagem destinatarios { get; set; }
        public string Assunto { get; set; }
        public string Corpo { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime? DataRecebimento { get; set; }
        public DateTime? DataLeitura { get; set; }
    }
}
