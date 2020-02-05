using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class VeiculoTaxistaService : ServiceBase<VeiculoTaxista, VeiculoTaxistaSummary, Guid>, IVeiculoTaxistaService
    {
        private readonly IVeiculoTaxistaRepository _VeiculoTaxistaRepository;
        private readonly ITaxistaRepository _taxistaRepository;

        public VeiculoTaxistaService(IVeiculoTaxistaRepository VeiculoTaxistaRepository, ITaxistaRepository taxistaRepository)
        {
            _VeiculoTaxistaRepository = VeiculoTaxistaRepository;
            _taxistaRepository = taxistaRepository;
        }
		
		public override string GetTag()
        {
            return "veiculo_taxista";
        }
        
        public async Task<IEnumerable<VeiculoTaxistaSummary>> ConsultaVeiculosDeTaxista(Guid id)
        {
            var veiculosTaxistas = await _VeiculoTaxistaRepository.Search(x => x.IdTaxista == id);
            return await GetAllSummariesAsync(veiculosTaxistas);
        }

        public async Task<bool> IsTaxiAtivoEmUsoPorOutroTaxista(Guid id)
        {
            var veiculoAtivoTaxista = (await _VeiculoTaxistaRepository.Search(
                x => x.IdTaxista == id && x.Ativo, 
                new[] {"Veiculo", "Veiculo.Taxistas"})).FirstOrDefault(); // veículo ativo do taxista

            if (veiculoAtivoTaxista != null) // taxista tem um veículo ativo?
            {
                var taxistasVeiculoAtivo = veiculoAtivoTaxista.Veiculo.Taxistas; // todos os taxistas associados a este veículo
                return taxistasVeiculoAtivo.Any(x =>
                    x.Taxista.Id != id && // outro taxista ...
                    x.Taxista.Ativo && // .. que esteja ativo ...
                    x.Taxista.Disponivel && // e disponível ...
                    x.Ativo);  // ... e que o veículo esteja ativo para este outro taxista
            }

            return false;
        }

        protected override Task<VeiculoTaxista> CreateEntryAsync(VeiculoTaxistaSummary summary)
        {
            return Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new VeiculoTaxista
                {
                    Id = summary.Id,
                    IdVeiculo = summary.IdVeiculo,
                    IdTaxista = summary.IdTaxista,
                    Ativo = summary.Ativo
                };
            });
        }

        protected override async Task<VeiculoTaxistaSummary> CreateSummaryAsync(VeiculoTaxista entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;

                return new VeiculoTaxistaSummary
                {
                    Id = entry.Id,
                    IdVeiculo = entry.IdVeiculo,
                    IdTaxista = entry.IdTaxista,
                    Ativo = entry.Ativo
                };
            });
        }

        protected override Guid GetKeyFromSummary(VeiculoTaxistaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<VeiculoTaxista> GetRepository()
        {
            return _VeiculoTaxistaRepository;
        }

        protected override void UpdateEntry(VeiculoTaxista entry, VeiculoTaxistaSummary summary)
        {
            entry.IdVeiculo = summary.IdVeiculo;
            entry.IdTaxista = summary.IdTaxista;
            entry.Ativo = summary.Ativo;
        }

        protected override void ValidateSummary(VeiculoTaxistaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "VeiculoTaxista: sumário é obrigatório"));
            }

            if (summary.IdVeiculo.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdVeiculo", "VeiculoTaxista: veículo inexistente ou não informado"));
            }

            if (summary.IdTaxista.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdTaxista", "VeiculoTaxista: taxista inexistente ou não informado"));
            }
        }
    }
}
