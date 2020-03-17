using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Veiculo;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Infraestructure.Entries.Piloto;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class RegistroVeiculoService : ServiceBase<RegistroVeiculo, RegistroVeiculoSummary, Guid>, IRegistroVeiculoService
    {
        private readonly IRegistroVeiculoRepository _RegistroVeiculoRepository;

        public RegistroVeiculoService(IRegistroVeiculoRepository RegistroVeiculoRepository)
        {
            _RegistroVeiculoRepository = RegistroVeiculoRepository;
        }

        public override string GetTag()
        {
            return "RegistroVeiculo";
        }

        protected override async Task<RegistroVeiculo> CreateEntryAsync(RegistroVeiculoSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new RegistroVeiculo
                {
                    Id = summary.Id,
                    IdFotoCRLV = summary.IdFotoCRLV,
                    IdFotoFrente = summary.IdFotoFrente,
                    IdFotoTras = summary.IdFotoTras,
                    IdFotoEsquerda = summary.IdFotoEsquerda,
                    IdFotoDireita = summary.IdFotoDireita,
                    IdUF = summary.IdUF,
                    Renavam = summary.Renavam,
                    AnoExercicio = summary.AnoExercicio,
                    Chassi = summary.Chassi
                };
            });
        }

        protected override async Task<RegistroVeiculoSummary> CreateSummaryAsync(RegistroVeiculo entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;
                return new RegistroVeiculoSummary
                {
                    Id = entry.Id,
                    IdFotoCRLV = entry.IdFotoCRLV,
                    IdFotoFrente = entry.IdFotoFrente,
                    IdFotoTras = entry.IdFotoTras,
                    IdFotoEsquerda = entry.IdFotoEsquerda,
                    IdFotoDireita = entry.IdFotoDireita,
                    IdUF = entry.IdUF,
                    Renavam = entry.Renavam,
                    AnoExercicio = entry.AnoExercicio,
                    Chassi = entry.Chassi
                };
            });
        }

        protected override Guid GetKeyFromSummary(RegistroVeiculoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<RegistroVeiculo> GetRepository()
        {
            return _RegistroVeiculoRepository;
        }

        protected override void UpdateEntry(RegistroVeiculo entry, RegistroVeiculoSummary summary)
        {
            entry.IdFotoCRLV = summary.IdFotoCRLV;
            entry.IdFotoFrente = summary.IdFotoFrente;
            entry.IdFotoTras = summary.IdFotoTras;
            entry.IdFotoEsquerda = summary.IdFotoEsquerda;
            entry.IdFotoDireita = summary.IdFotoDireita;
            entry.IdUF = summary.IdUF;
            entry.Renavam = summary.Renavam;
            entry.AnoExercicio = summary.AnoExercicio;
            entry.Chassi = summary.Chassi;
        }

        protected override void ValidateSummary(RegistroVeiculoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "RegistroVeiculo: sumário é obrigatório"));
            }
        }
    }
}
