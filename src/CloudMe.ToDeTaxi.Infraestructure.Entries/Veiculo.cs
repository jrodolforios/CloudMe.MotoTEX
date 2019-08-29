﻿using System;
using System.Collections.Generic;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Veiculo : EntryBase
    {
        public Guid Id { get; set; }

        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int Capacidade { get; set; }
        public int Cor { get; set; }
        public byte[] Foto { get; set; }

        public virtual IEnumerable<Corrida> Corridas { get; set; }
        public virtual IEnumerable<VeiculoTaxista> Taxistas { get; set; }
    }
}