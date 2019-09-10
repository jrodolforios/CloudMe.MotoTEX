using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Veiculo;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class VeiculoService : ServiceBase<Veiculo, VeiculoSummary, Guid>, IVeiculoService
    {
        private readonly IVeiculoRepository _VeiculoRepository;

        public VeiculoService(IVeiculoRepository VeiculoRepository)
        {
            _VeiculoRepository = VeiculoRepository;
        }

        protected override Task<Veiculo> CreateEntryAsync(VeiculoSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Veiculo = new Veiculo
            {
                Id = summary.Id,
                Placa = summary.Placa,
                Marca = summary.Marca,
                Modelo = summary.Modelo,
                Capacidade = summary.Capacidade,
                Cor = summary.Cor,
                IdFoto = summary.IdFoto
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
                Capacidade = entry.Capacidade,
                Cor = entry.Cor,
                IdFoto = entry.IdFoto
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
            entry.Placa = summary.Placa;
            entry.Marca = summary.Marca;
            entry.Modelo = summary.Modelo;
            entry.Capacidade = summary.Capacidade;
            entry.Cor = summary.Cor;
            entry.IdFoto = summary.IdFoto;
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

            if (summary.Capacidade < 2)
            {
                this.AddNotification(new Notification("Modelo", "Veiculo: capacidade inconsistente"));
            }
        }
    }
}
