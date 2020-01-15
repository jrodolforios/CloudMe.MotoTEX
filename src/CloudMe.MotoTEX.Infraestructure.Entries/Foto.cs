using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Entries
{
    public class Foto : EntryBase<Guid>
    {
        //public Guid Id { get; set; }
        public string Nome { get; set; }
        public string NomeArquivo { get; set; }
        public byte[] Dados { get; set; }
    }
}
