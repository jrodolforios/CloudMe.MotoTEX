using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Corrida
{
    public class EstatisticasSolicitacoesCorrida
    {
        public int Total { get; set; }
        public int Agendadas { get; set; }
        public int Interurbanas { get; set; }
        public float ValorMedio { get; set; }
        public int EmAndamento { get; set; }
        public int Atendidas { get; set; }
        public int NaoAtendidas { get; set; }
    };
}
