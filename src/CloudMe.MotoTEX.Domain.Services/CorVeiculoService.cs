using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Veiculo;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class CorVeiculoService : ServiceBase<CorVeiculo, CorVeiculoSummary, Guid>, ICorVeiculoService
    {
        private readonly ICorVeiculoRepository _CorVeiculoRepository;

        public CorVeiculoService(ICorVeiculoRepository CorVeiculoRepository)
        {
            _CorVeiculoRepository = CorVeiculoRepository;
        }

        public override string GetTag()
        {
            return "cor_veiculo";
        }

        protected override Task<CorVeiculo> CreateEntryAsync(CorVeiculoSummary summary)
        {
            return Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new CorVeiculo
                {
                    Id = summary.Id,
                    Nome = summary.Nome
                };
            });
        }

        protected override Task<CorVeiculoSummary> CreateSummaryAsync(CorVeiculo entry)
        {
            return Task.Run(() =>
            {
                if (entry == null) return default;

                return new CorVeiculoSummary
                {
                    Id = entry.Id,
                    Nome = entry.Nome
                };
            });
        }

        protected override Guid GetKeyFromSummary(CorVeiculoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<CorVeiculo> GetRepository()
        {
            return _CorVeiculoRepository;
        }

        protected override void UpdateEntry(CorVeiculo entry, CorVeiculoSummary summary)
        {
            entry.Nome = summary.Nome;
        }

        protected override void ValidateSummary(CorVeiculoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Cor de veículo: sumário é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.Nome))
            {
                this.AddNotification(new Notification("Nome", "Cor de veículo: nome é obrigatório"));
            }
        }
    }
}
