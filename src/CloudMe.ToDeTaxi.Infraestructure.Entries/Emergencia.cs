using CloudMe.ToDeTaxi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Emergencia: EntryBase<Guid>
    {
        public Guid IdTaxista { get; set; }
        public virtual Taxista Taxista { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public StatusEmergencia Status { get; set; }
    }
}
