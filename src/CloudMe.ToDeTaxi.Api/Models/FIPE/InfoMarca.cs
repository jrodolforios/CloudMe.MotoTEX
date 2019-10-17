using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Api.Models.FIPE
{
    public class InfoMarca
    {
        public virtual IEnumerable<AnoModelo> anos { get; set; }
        public virtual IEnumerable<ModeloVeiculo> modelos { get; set; }
    }
}
