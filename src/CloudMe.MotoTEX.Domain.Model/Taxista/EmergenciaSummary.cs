using CloudMe.MotoTEX.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Taxista
{
    public class EmergenciaSummary
    {
        public Guid Id { get; set; }
        public Guid IdTaxista { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public StatusEmergencia Status { get; set; }
    }
}
