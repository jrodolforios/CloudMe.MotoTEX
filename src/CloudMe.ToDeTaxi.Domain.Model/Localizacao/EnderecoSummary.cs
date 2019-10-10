using System;

namespace CloudMe.ToDeTaxi.Domain.Model.Localizacao
{
    public class EnderecoSummary
    {
        public Guid Id { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public Guid? IdLocalizacao { get; set; }
    }
}
