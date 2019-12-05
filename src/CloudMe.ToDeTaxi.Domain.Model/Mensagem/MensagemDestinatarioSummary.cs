using CloudMe.ToDeTaxi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Mensagem
{
    public class MensagemDestinatarioSummary
    {
        public Guid Id { get; set; }
        public Guid IdMensagem { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid? IdGrupoUsuario { get; set; } // nulo se particular/privado
        public DateTime DataRecebimento { get; set; }
        public DateTime DataLeitura { get; set; }
        public StatusMensagem Status { get; set; }
    }
}
