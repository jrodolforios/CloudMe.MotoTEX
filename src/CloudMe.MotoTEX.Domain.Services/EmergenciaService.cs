using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using CloudMe.MotoTEX.Domain.Enums;
using CloudMe.MotoTEX.Domain.Notifications.Abstracts;

namespace CloudMe.MotoTEX.Domain.Services
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

        protected override async Task<Emergencia> CreateEntryAsync(EmergenciaSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new Emergencia
                {
                    Id = summary.Id,
                    IdTaxista = summary.IdTaxista,
                    Latitude = summary.Latitude,
                    Longitude = summary.Longitude,
                    Status = summary.Status,
                };
            });
        }

        protected override async Task<EmergenciaSummary> CreateSummaryAsync(Emergencia entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;

                return new EmergenciaSummary
                {
                    Id = entry.Id,
                    IdTaxista = entry.IdTaxista,
                    Latitude = entry.Latitude,
                    Longitude = entry.Longitude,
                    Status = entry.Status
                };
            });
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

        public async Task<bool> Panico(Guid id_taxista, string latitude, string longitude)
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
