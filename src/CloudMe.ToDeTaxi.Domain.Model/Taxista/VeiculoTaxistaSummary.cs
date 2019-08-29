using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Taxista
{
    public class VeiculoTaxistaSummary
    {
        public Guid Id { get; set; }
        public Guid IdVeiculo { get; set; }
        public Guid IdTaxista { get; set; }
        public bool Ativo { get; set; }
    }
}
