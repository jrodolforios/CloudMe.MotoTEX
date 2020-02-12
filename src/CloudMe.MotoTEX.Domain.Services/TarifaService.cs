using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using CloudMe.MotoTEX.Domain.Model.Taxista.Integracoes;
using System.Linq;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class TarifaService : ServiceBase<Tarifa, TarifaSummary, Guid>, ITarifaService
    {
        private readonly ITarifaRepository _TarifaRepository;

        public TarifaService(ITarifaRepository TarifaRepository)
        {
            _TarifaRepository = TarifaRepository;
        }

        public override string GetTag()
        {
            return "tarifa";
        }

        public async Task<decimal> GetValorCorrida(DateTime date, decimal kilometers)
        {
            decimal valorAPagar = 0.0M;

            // TODO: Modelar corretamente a tarifação
            if (kilometers <= 4)
            {
                valorAPagar = 6.0M;
            }
            else if (kilometers > 4 && kilometers <= 6)
            {
                valorAPagar = 7.0M;
            }
            else // kilometers > 6
            {
                valorAPagar = 8.0M;
            }

            //var tarifa = (await _TarifaRepository.FindAll()).FirstOrDefault();

            RestClient client = new RestClient("https://api.calendario.com.br/");

            RestRequest request = new RestRequest(string.Empty, Method.GET);
            request.AddQueryParameter("ano", DateTime.Now.Year.ToString());
            request.AddQueryParameter("cidade", "TEIXEIRA_DE_FREITAS");
            request.AddQueryParameter("token", "cm9kb2xmby5yaW9zQGNsb3VkbWUuY29tLmJyJmhhc2g9NTQ5MjMxMzA");
            request.AddQueryParameter("json", "true");

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var feriados = JsonConvert.DeserializeObject<IList<Feriado>>(response.Content);

                if (feriados.Any(x => x.Date == date.ToString("dd/MM/yyyy") && (x.Type.ToLower() == "feriado nacional" || x.Type.ToLower() == "feriado municipal"))
                    || date.DayOfWeek == DayOfWeek.Sunday || HorarioNoturno(date))
                {
                    //valorAPagar += (decimal)tarifa.KmRodadoBandeira2;
                    valorAPagar += 1.0M;

                    //return valorAPagar;
                }
                /*else
                {
                    if (kilometers > 4)
					{
                        valorAPagar += (decimal)tarifa.KmRodadoBandeira1;
					}

                    return valorAPagar;
                }*/
            }

            return valorAPagar;
        }

        private bool HorarioNoturno(DateTime date)
        {
            // convert everything to TimeSpan
            TimeSpan start = new TimeSpan(20, 0, 0);
            TimeSpan end = new TimeSpan(06, 0, 0);
            TimeSpan now = date.TimeOfDay;
            // see if start comes before end
            if (start < end)
            {
                return start <= now && now <= end;
            }
            // start is after end, so do the inverse comparison
            return !(end <= now && now <= start);
        }

        protected override async Task<Tarifa> CreateEntryAsync(TarifaSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new Tarifa
                {
                    Id = summary.Id,
                    Bandeirada = summary.Bandeirada,
                    KmRodadoBandeira1 = summary.KmRodadoBandeira1,
                    KmRodadoBandeira2 = summary.KmRodadoBandeira2,
                    HoraParada = summary.HoraParada
                };
            });
        }

        protected override async Task<TarifaSummary> CreateSummaryAsync(Tarifa entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;

                return new TarifaSummary
                {
                    Id = entry.Id,
                    Bandeirada = entry.Bandeirada,
                    KmRodadoBandeira1 = entry.KmRodadoBandeira1,
                    KmRodadoBandeira2 = entry.KmRodadoBandeira2,
                    HoraParada = entry.HoraParada
                };
            });
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
