﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class VeiculoTaxista : EntryBase
    {
        public Guid Id { get; set; }

        public Guid IdVeiculo { get; set; }
        public virtual Veiculo Veiculo { get; set; }

        public Guid IdTaxista { get; set; }
        public virtual Taxista Taxista { get; set; }

        public bool Ativo { get; set; }
    }
}
