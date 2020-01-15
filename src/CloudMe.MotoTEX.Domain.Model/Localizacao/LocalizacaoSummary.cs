using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Localizacao
{
    public class LocalizacaoSummary
    {
        public Guid Id { get; set; }
        public Guid? IdUsuario { get; set; }
        public string Endereco { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string NomePublico { get; set; }
    }
}
