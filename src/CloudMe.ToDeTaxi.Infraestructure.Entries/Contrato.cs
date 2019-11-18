using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Contrato : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public string Conteudo { get; set; }
        public bool UltimaVersao { get; set; }
    }
}
