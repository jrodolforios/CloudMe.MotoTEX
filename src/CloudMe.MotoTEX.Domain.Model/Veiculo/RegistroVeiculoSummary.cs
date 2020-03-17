using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Veiculo
{
    public class RegistroVeiculoSummary
    {
        public Guid Id { get; set; }
        public Guid IdVeiculo { get; set; }
        public Guid IdFotoCRLV { get; set; }
        public Guid IdFotoFrente { get; set; }
        public Guid IdFotoTras { get; set; }
        public Guid IdFotoEsquerda { get; set; }
        public Guid IdFotoDireita { get; set; }
        public Guid IdUF { get; set; }
        public int AnoExercicio { get; set; }
        public string Chassi { get; set; }
        public string Renavam { get; set; }
    }
}
