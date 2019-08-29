using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class TarifaService : ServiceBase<Tarifa, TarifaSummary, Guid>, ITarifaService
    {
        private readonly ITarifaRepository _TarifaRepository;

        public TarifaService(ITarifaRepository TarifaRepository)
        {
            _TarifaRepository = TarifaRepository;
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
