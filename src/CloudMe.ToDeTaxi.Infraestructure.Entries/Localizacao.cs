using System;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Localizacao : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public string Endereco { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public string NomePublico { get; set; }
    }
}
