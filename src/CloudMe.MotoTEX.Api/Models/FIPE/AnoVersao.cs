using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Api.Models.FIPE
{
    public class AnoVersao
    {
        public string codigo { get { return Value; } }
        public string nome { get { return Label; } }
        public string Label { get; set; }
        public string Value { get; set; }
        public string ano { get; set; }
        public string versao { get; set; }
    }
}
