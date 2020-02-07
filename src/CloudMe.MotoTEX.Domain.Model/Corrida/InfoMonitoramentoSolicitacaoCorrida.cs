using CloudMe.MotoTEX.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Corrida
{
    public class InfoMonitoramentoSolicitacaoCorrida
    {
        public Guid IdSolicitacao { get; set; }
        public StatusMonitoramentoSolicitacaoCorrida StatusMonitoramento { get; set; }
        public double RaioAtivacao { get; set; }
    }
}
