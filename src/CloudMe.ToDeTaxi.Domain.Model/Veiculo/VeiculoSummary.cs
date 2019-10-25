using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Veiculo
{
    public class VeiculoSummary
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Capacidade { get; set; }
        public string Cor { get; set; }
        public Guid? IdFoto { get; set; }
    }
}
