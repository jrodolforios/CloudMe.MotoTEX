using System;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;

namespace CloudMe.ToDeTaxi.Domain.Model.Taxista
{
    public class PontoTaxiSummary
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public EnderecoSummary Endereco { get; set; }
    }
}
