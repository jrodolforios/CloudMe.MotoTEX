using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using CloudMe.ToDeTaxi.Domain.Model.Taxista.Integracoes;
using System.Linq;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class TarifaService : ServiceBase<Tarifa, TarifaSummary, Guid>, ITarifaService
    {
        private readonly ITarifaRepository _TarifaRepository;

        public TarifaService(ITarifaRepository TarifaRepository)
        {
            _TarifaRepository = TarifaRepository;
        }

        public decimal GetValorKmRodadoAtual(DateTime date)
        {
            var tarifa = _TarifaRepository.FindAll().FirstOrDefault();

            RestClient client = new RestClient("https://api.calendario.com.br/");

            RestRequest request = new RestRequest(string.Empty, Method.GET);
            request.AddQueryParameter("ano", DateTime.Now.Year.ToString());
            request.AddQueryParameter("cidade", "TEOFILO_OTONI");
            request.AddQueryParameter("token", "cm9kb2xmby5yaW9zQGNsb3VkbWUuY29tLmJyJmhhc2g9NTQ5MjMxMzA");
            request.AddQueryParameter("json", "true");

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var feriados = JsonConvert.DeserializeObject<IList<Feriado>>(response.Content);

                if (feriados.Any(x => x.Date == date.ToString("dd/MM/yyyy") && (x.Type.ToLower() == "feriado nacional" || x.Type.ToLower() == "feriado municipal"))
                    || date.DayOfWeek == DayOfWeek.Sunday || HorarioNoturno(date))
                    return (decimal)tarifa.KmRodadoBandeira2;
                else
                    return (decimal)tarifa.KmRodadoBandeira1;
            }
            else
                return 0;
        }

        private bool HorarioNoturno (DateTime date)
        {
            // convert everything to TimeSpan
            TimeSpan start = new TimeSpan(22, 0, 0);
            TimeSpan end = new TimeSpan(06, 0, 0);
            TimeSpan now = date.TimeOfDay;
            // see if start comes before end
            if (start < end)
                return start <= now && now <= end;
            // start is after end, so do the inverse comparison
            return !(end <= now && now <= start);
        }

        protected override Task<Tarifa> CreateEntryAsync(TarifaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Tarifa = new Tarifa
            {
                Id = summary.Id,
                Bandeirada = summary.Bandeirada,
                KmRodadoBandeira1 = summary.KmRodadoBandeira1,
                KmRodadoBandeira2 = summary.KmRodadoBandeira2,
                HoraParada = summary.HoraParada
            };
            return Task.FromResult(Tarifa);
        }

        protected override Task<TarifaSummary> CreateSummaryAsync(Tarifa entry)
        {
            var Tarifa = new TarifaSummary
            {
                Id = entry.Id,
                Bandeirada = entry.Bandeirada,
                KmRodadoBandeira1 = entry.KmRodadoBandeira1,
                KmRodadoBandeira2 = entry.KmRodadoBandeira2,
                HoraParada = entry.HoraParada
            };

            return Task.FromResult(Tarifa);
        }

        protected override Guid GetKeyFromSummary(TarifaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Tarifa> GetRepository()
        {
            return _TarifaRepository;
        }

        protected override void UpdateEntry(Tarifa entry, TarifaSummary summary)
        {
            entry.Bandeirada = summary.Bandeirada;
            entry.KmRodadoBandeira1 = summary.KmRodadoBandeira1;
            entry.KmRodadoBandeira2 = summary.KmRodadoBandeira2;
            entry.HoraParada = summary.HoraParada;
        }

        protected override void ValidateSummary(TarifaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Tarifa: sumário é obrigatório"));
            }

            if (summary.Bandeirada < 0)
            {
                this.AddNotification(new Notification("Bandeirada", "Tarifa: valor de bandeirada inválido"));
            }

            if (summary.KmRodadoBandeira1 < 0)
            {
                this.AddNotification(new Notification("KmRodadoBandeira1", "Tarifa: valor do quilômetro rodado na bandeira 1 inválido"));
            }

            if (summary.KmRodadoBandeira2 < 0)
            {
                this.AddNotification(new Notification("KmRodadoBandeira2", "Tarifa: valor do quilômetro rodado na bandeira 2 inválido"));
            }

            if (summary.HoraParada < 0)
            {
                this.AddNotification(new Notification("HoraParada", "Tarifa: valor da hora parada inválido"));
            }
        }
    }
}
