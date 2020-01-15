using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model
{
    public class ContratoSummary
    {
        public Guid Id { get; set; }
        public string Conteudo { get; set; }
        public bool UltimaVersao { get; set; }
    }
}
