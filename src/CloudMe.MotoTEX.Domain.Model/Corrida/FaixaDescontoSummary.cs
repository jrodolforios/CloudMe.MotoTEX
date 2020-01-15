using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Corrida
{
    public class FaixaDescontoSummary
    {
        public Guid Id { get; set; }
        public float Valor { get; set; }
        public string Descricao { get; set; }
    }
}
