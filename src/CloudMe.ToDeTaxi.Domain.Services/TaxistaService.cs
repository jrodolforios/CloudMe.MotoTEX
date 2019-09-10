using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class TaxistaService : ServiceBase<Taxista, TaxistaSummary, Guid>, ITaxistaService
    {
        private readonly ITaxistaRepository _TaxistaRepository;

        public TaxistaService(ITaxistaRepository TaxistaRepository)
        {
            _TaxistaRepository = TaxistaRepository;
        }

        protected override Task<Taxista> CreateEntryAsync(TaxistaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Taxista = new Taxista
            {
                Id = summary.Id,
                IdUsuario = summary.IdUsuario,
                IdEndereco = summary.IdEndereco,
                IdLocalizacaoAtual = summary.IdLocalizacaoAtual,
                IdFoto = summary.IdFoto
            };
            return Task.FromResult(Taxista);
        }

        protected override Task<TaxistaSummary> CreateSummaryAsync(Taxista entry)
        {
            var Taxista = new TaxistaSummary
            {
                Id = entry.Id,
                IdUsuario = entry.IdUsuario,
                IdEndereco = entry.IdEndereco,
                IdLocalizacaoAtual = entry.IdLocalizacaoAtual,
                IdFoto = entry.IdFoto
            };

            return Task.FromResult(Taxista);
        }

        protected override Guid GetKeyFromSummary(TaxistaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Taxista> GetRepository()
        {
            return _TaxistaRepository;
        }

        protected override void UpdateEntry(Taxista entry, TaxistaSummary summary)
        {
            entry.IdUsuario = summary.IdUsuario;
            entry.IdEndereco = summary.IdEndereco;
            entry.IdLocalizacaoAtual = summary.IdLocalizacaoAtual;
            entry.IdFoto = summary.IdFoto;
        }

        protected override void ValidateSummary(TaxistaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Taxista: sumário é obrigatório"));
            }

            if (summary.IdUsuario.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdUsuario", "Taxista: usuário inexistente ou não informado"));
            }

            if (summary.IdEndereco.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdEndereco", "Taxista: endereço inexistente ou não informado"));
            }
        }
    }
}
