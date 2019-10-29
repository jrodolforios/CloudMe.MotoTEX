using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Taxista.Integracoes
{
    public class Feriado
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type_code")]
        public string TypeCode { get; set; }

        [JsonProperty("raw_description", NullValueHandling = NullValueHandling.Ignore)]
        public string RawDescription { get; set; }
    }

    public enum TypeEnum { DiaConvencional, Facultativo, FeriadoMunicipal, FeriadoNacional };

}
