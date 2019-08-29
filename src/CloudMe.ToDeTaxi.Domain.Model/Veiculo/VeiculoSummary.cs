using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Veiculo
{
    public class VeiculoSummary
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int Capacidade { get; set; }
        public int Cor { get; set; }
        public byte[] Foto { get; set; }
    }
}
