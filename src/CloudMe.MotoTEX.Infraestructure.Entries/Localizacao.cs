using GeoCoordinatePortable;
using System;
using System.Globalization;

namespace CloudMe.MotoTEX.Infraestructure.Entries
{
    public class Localizacao : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public string Endereco { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public double Angulo { get; set; } = 0; // (graus) aponta para o norte

        public string NomePublico { get; set; }

        public Guid? IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public static double ObterDistancia(Localizacao origem, Localizacao destino)
        {
            GeoCoordinate pin1 = new GeoCoordinate(
                Convert.ToDouble(origem.Latitude, CultureInfo.InvariantCulture.NumberFormat),
                Convert.ToDouble(origem.Longitude, CultureInfo.InvariantCulture.NumberFormat));

            GeoCoordinate pin2 = new GeoCoordinate(
                Convert.ToDouble(destino.Latitude, CultureInfo.InvariantCulture.NumberFormat),
                Convert.ToDouble(destino.Longitude, CultureInfo.InvariantCulture.NumberFormat));

            return pin1.GetDistanceTo(pin2);
        }
    }
}
