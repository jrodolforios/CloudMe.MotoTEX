﻿using prmToolkit.NotificationPattern;
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
        
        public Task<List<VeiculoTaxistaSummary>> ConsultaVeiculosDeTaxista(Guid id)
        {
            var veiculosTaxistas = _VeiculoTaxistaRepository.FindAll().Where(x => x.IdTaxista == id).ToList();
            var veiculosTaxistasSummaries = new List<VeiculoTaxistaSummary>();


            veiculosTaxistas.ForEach(async x =>
            {
                veiculosTaxistasSummaries.Add(await CreateSummaryAsync(x));
            });

            return Task.FromResult(veiculosTaxistasSummaries);
        }

        public bool IsTaxiAtivoEmUsoPorOutroTaxista(Guid id)
        {

            if(_VeiculoTaxistaRepository.FindAll().Any(x => x.IdTaxista == id)) { 
            var veiculosTaxista = _VeiculoTaxistaRepository.FindAll().Where(x => x.IdTaxista == id && x.Ativo).ToList();

            var veiculoTaxistas = _VeiculoTaxistaRepository.FindAll().Where(x => veiculosTaxista.Any(y => y.IdVeiculo == x.IdVeiculo && y.IdTaxista != id && y.Ativo)).ToList();



            return _taxistaRepository.FindAll().Any(x => veiculoTaxistas.Any(y => y.IdTaxista == x.Id && x.Ativo && x.Disponivel));
            }
            else
            {
                return true;
            }
        }

        protected override Task<VeiculoTaxista> CreateEntryAsync(VeiculoTaxistaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var VeiculoTaxista = new VeiculoTaxista
            {
                Id = summary.Id,
                IdVeiculo = summary.IdVeiculo,
                IdTaxista = summary.IdTaxista,
                Ativo = summary.Ativo
            };
            return Task.FromResult(VeiculoTaxista);
        }

        protected override Task<VeiculoTaxistaSummary> CreateSummaryAsync(VeiculoTaxista entry)
        {
            var VeiculoTaxista = new VeiculoTaxistaSummary
            {
                Id = entry.Id,
                IdVeiculo = entry.IdVeiculo,
                IdTaxista = entry.IdTaxista,
                Ativo = entry.Ativo
            };

            return Task.FromResult(VeiculoTaxista);
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