using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Passageiro
{
    public class FavoritoSummary
    {
        public Guid Id { get; set; }
        public Guid IdPassageiro { get; set; }
        public Guid IdTaxista { get; set; }
        public int Preferencia { get; set; }
    }
}
