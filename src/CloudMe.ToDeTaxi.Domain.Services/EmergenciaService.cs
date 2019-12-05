using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using CloudMe.ToDeTaxi.Domain.Enums;
using CloudMe.ToDeTaxi.Domain.Notifications.Abstracts;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class EmergenciaService : ServiceBase<Emergencia, EmergenciaSummary, Guid>, IEmergenciaService
    {
        private readonly IEmergenciaRepository _EmergenciaRepository;
        private readonly IPoolLocalizacaoTaxista _PoolLocalizacaoTaxista;

        public EmergenciaService(
            IEmergenciaRepository EmergenciaRepository,
            IPoolLocalizacaoTaxista PoolLocalizacaoTaxista)
        {
            _EmergenciaRepository = EmergenciaRepository;
            _PoolLocalizacaoTaxista = PoolLocalizacaoTaxista;
        }

        public override string GetTag()
        {
            return "emergencia";
        }

        protected override Task<Emergencia> CreateEntryAsync(EmergenciaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Emergencia = new Emergencia
            {
                Id = summary.Id,
                IdTaxista = summary.IdTaxista,
                Latitude = summary.Latitude,
                Longitude = summary.Longitude,
                Status = summary.Status,
            };
            return Task.FromResult(Emergencia);
        }

        protected override Task<EmergenciaSummary> CreateSummaryAsync(Emergencia entry)
        {
            var Emergencia = new EmergenciaSummary
            {
                Id = entry.Id,
                IdTaxista = entry.IdTaxista,
                Latitude = entry.Latitude,
                Longitude = entry.Longitude,
                Status = entry.Status
            };

            return Task.FromResult(Emergencia);
        }

        protected override Guid GetKeyFromSummary(EmergenciaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Emergencia> GetRepository()
        {
            return _EmergenciaRepository;
        }

        protected override void UpdateEntry(Emergencia entry, EmergenciaSummary summary)
        {
            entry.Status = summary.Status;
        }

        protected override void ValidateSummary(EmergenciaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Emergencia: sumário é obrigatório"));
            }
        }

        public async Task<bool> Panico(Guid id_taxista, string longitude, string latitude)
        {
            var emergenciaSummary = new EmergenciaSummary()
            {
                IdTaxista = id_taxista,
                Longitude = longitude,
                Latitude = latitude,
                Status = StatusEmergencia.Iniciada,
            };

            var emergencia = await CreateAsync(emergenciaSummary);
            if (emergencia == null)
            {
                return false;
            }

            await _PoolLocalizacaoTaxista.EnviarPanico(emergenciaSummary);

            return true;
        }

        public async Task<bool> AlterarStatus(Guid id_emergencia, StatusEmergencia status)
        {
            var emergencia = await _EmergenciaRepository.FindByIdAsync(id_emergencia);
            if(emergencia == null)
            {
                AddNotification(new Notification("Alterar status emergência", "registro de emergência não encontrado"));
                return false;
            }

            emergencia.Status = status;
            return await _EmergenciaRepository.ModifyAsync(emergencia);
        }
    }
}
