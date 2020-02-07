using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Corrida
{
    public class FaixaAtivacaoSummary
    {
        public Guid Id { get; set; }
        public double Raio { get; set; }
        public int Janela { get; set; }
    }
}
