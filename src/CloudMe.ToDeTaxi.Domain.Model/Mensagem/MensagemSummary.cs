using CloudMe.ToDeTaxi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Mensagem
{
    public class MensagemSummary
    {
        public Guid Id { get; set; }
        public Guid IdRemetente { get; set; }
        public string Assunto { get; set; }
        public string Corpo { get; set; }
    }
}
