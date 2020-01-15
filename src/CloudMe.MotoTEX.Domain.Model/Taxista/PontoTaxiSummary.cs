using System;
using CloudMe.MotoTEX.Domain.Model.Localizacao;

namespace CloudMe.MotoTEX.Domain.Model.Taxista
{
    public class PontoTaxiSummary
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public EnderecoSummary Endereco { get; set; }
    }
}
