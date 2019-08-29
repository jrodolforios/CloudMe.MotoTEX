using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Taxista
{
    public class TaxistaSummary
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdEndereco { get; set; }
        public Guid? IdLocalizacaoAtual { get; set; }
        public byte[] Foto { get; set; }
    }
}
