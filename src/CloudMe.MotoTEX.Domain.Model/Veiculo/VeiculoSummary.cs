using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Veiculo
{
    public class VeiculoSummary
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public string Versao { get; set; }
        public Guid? IdFoto { get; set; }
        public Guid? IdCorVeiculo { get; set; }
    }
}
