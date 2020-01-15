using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Api.Models.FIPE
{
    public class InfoMarca
    {
        public class AnoModelo
        {
            public string codigo { get; set; }
            public string nome { get; set; }
        }

        public virtual IEnumerable<AnoModelo> anos { get; set; }
        public virtual IEnumerable<ModeloVeiculo> modelos { get; set; }
    }
}
