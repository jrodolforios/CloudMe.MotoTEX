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
                Modelo = summary.Modelo,
                Capacidade = summary.Capacidade,
                Cor = summary.Cor,
                Foto = summary.Foto.ToArray()
            };
            return Task.FromResult(Veiculo);
        }

        protected override Task<VeiculoSummary> CreateSummaryAsync(Veiculo entry)
        {
            var Veiculo = new VeiculoSummary
            {
                Id = entry.Id,
                Placa = entry.Placa,
                Modelo = entry.Modelo,
                Capacidade = entry.Capacidade,
                Cor = entry.Cor,
                Foto = entry.Foto.ToArray()
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
            entry.Modelo = summary.Modelo;
            entry.Capacidade = summary.Capacidade;
            entry.Cor = summary.Cor;
            entry.Foto = summary.Foto.ToArray();
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

            if (string.IsNullOrEmpty(summary.Modelo))
            {
                this.AddNotification(new Notification("Modelo", "Veiculo: modelo não informado"));
            }

            if (summary.Capacidade < 2)
            {
                this.AddNotification(new Notification("Modelo", "Veiculo: capacidade inconsistente"));
            }

            if (summary.Foto == null || summary.Foto.Length == 0)
            {
                this.AddNotification(new Notification("Foto", "Veiculo: foto é obrigatória"));
            }
        }
    }
}
