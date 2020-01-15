using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Foto
{
    public class FotoSummary
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string NomeArquivo { get; set; }
        public byte[] Dados { get; set; }
    }
}
