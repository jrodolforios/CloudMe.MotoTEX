using System;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;

namespace CloudMe.ToDeTaxi.Domain.Model.Taxista
{
    public class PontoTaxiSummary
    {
        public Guid Id { get; set; }
        public String Nome { get; set; }

        public LocalizacaoSummary Endereco { get; set; }
    }
}
