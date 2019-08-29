using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Corrida
{
    public class TarifaSummary
    {
        public Guid Id { get; set; }
        public float Bandeirada { get; set; }
        public float KmRodadoBandeira1 { get; set; }
        public float KmRodadoBandeira2 { get; set; }
        public float HoraParada { get; set; }
    }
}
