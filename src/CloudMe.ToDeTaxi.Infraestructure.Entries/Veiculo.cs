using System;
using System.Collections.Generic;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Veiculo : EntryBase
    {
        public Guid Id { get; set; }

        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public string Versao { get; set; }
        public int Capacidade { get; set; } = 4; // deprecated
        public string Cor { get; set; } // deprecated - usar CorVeiculo

        public Guid? IdFoto { get; set; }
        public virtual Foto Foto { get; set; }

        public Guid? IdCorVeiculo { get; set; }
        public virtual CorVeiculo CorVeiculo { get; set; }

        public virtual IEnumerable<Corrida> Corridas { get; set; }
        public virtual IEnumerable<VeiculoTaxista> Taxistas { get; set; }
    }
}
