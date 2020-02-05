using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Corrida
{
    public class FaixaAtivacao
    {
        public double RaioInicial { get; set; }
        public double RaioFinal { get; set; }
        public int Janela { get; set; }
    }
}
