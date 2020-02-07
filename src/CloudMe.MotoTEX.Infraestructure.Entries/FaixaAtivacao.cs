using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Entries
{
    public class FaixaAtivacao: EntryBase<Guid>
    {
        public double Raio { get; set; }
        public int Janela { get; set; }
    }
}
