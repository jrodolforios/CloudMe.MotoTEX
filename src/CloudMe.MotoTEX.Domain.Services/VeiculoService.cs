using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Veiculo;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class VeiculoService : ServiceBase<Veiculo, VeiculoSummary, Guid>, IVeiculoService
    {
        private readonly IVeiculoRepository _VeiculoRepository;

        public VeiculoService(IVeiculoRepository VeiculoRepository)
        {
            _VeiculoRepository = VeiculoRepository;
        }

        public override string GetTag()
        {
            return "veiculo";
        }

        protected override Task<Veiculo> CreateEntryAsync(VeiculoSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            // verifica se existe outro veículo com a mesma placa
            var veicPlaca = _VeiculoRepository.Search(veic => veic.Placa == summary.Placa).FirstOrDefault();
            if (veicPlaca != null)
            {
                AddNotification("Veículos", string.Format("Placa '{0}' está sendo utilizada por outro veículo", summary.Placa));
                return Task.FromResult<Veiculo>(null);
            }

            var Veiculo = new Veiculo
            {
                Id = summary.Id,
                Placa = summary.Placa,
                Marca = summary.Marca,
                Modelo = summary.Modelo,
                Ano = summary.Ano,
                Versao = summary.Versao,
                IdFoto = summary.IdFoto,
                IdCorVeiculo = summary.IdCorVeiculo
            };
            return Task.FromResult(Veiculo);
        }

        protected override Task<VeiculoSummary> CreateSummaryAsync(Veiculo entry)
        {
            var Veiculo = new VeiculoSummary
            {
                Id = entry.Id,
                Placa = entry.Placa,
                Marca = entry.Marca,
                Modelo = entry.Modelo,
                Ano = entry.Ano,
                Versao = entry.Versao,
                IdFoto = entry.IdFoto,
                IdCorVeiculo = entry.IdCorVeiculo
            };

            return Task.FromResult(Veiculo);
        }

        protected override Guid GetKeyFromSummary(VeiculoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Veiculo> GetRepository()
        {
            return _VeiculoRepository;
        }

        protected override void UpdateEntry(Veiculo entry, VeiculoSummary summary)
        {
            // verifica se existe outro veículo com a mesma placa
            var veicPlaca = _VeiculoRepository.Search(veic => veic.Placa == summary.Placa && veic.Id != summary.Id).FirstOrDefault();
            if (veicPlaca != null)
            {
                AddNotification("Veículos", string.Format("Placa '{0}' está sendo utilizada por outro veículo", summary.Placa));
                return;
            }

            entry.Placa = summary.Placa;
            entry.Marca = summary.Marca;
            entry.Modelo = summary.Modelo;
            entry.Ano = summary.Ano;
            entry.Versao = summary.Versao;
            entry.IdFoto = summary.IdFoto;
            entry.IdCorVeiculo = summary.IdCorVeiculo;
        }

        protected override void ValidateSummary(VeiculoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Veiculo: sumário é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.Placa))
            {
                this.AddNotification(new Notification("Placa", "Veiculo: placa não informada"));
            }

            if (string.IsNullOrEmpty(summary.Marca))
            {
                this.AddNotification(new Notification("Marca", "marca: placa não informada"));
            }

            if (string.IsNullOrEmpty(summary.Modelo))
            {
                this.AddNotification(new Notification("Modelo", "Veiculo: modelo não informado"));
            }

            if (string.IsNullOrEmpty(summary.Ano))
            {
                this.AddNotification(new Notification("Ano", "Veiculo: ano do veículo não informado"));
            }

            /*if (string.IsNullOrEmpty(summary.Versao))
            {
                this.AddNotification(new Notification("Versão", "Veiculo: versão do veículo não informada"));
            }*/
        }
    }
}
