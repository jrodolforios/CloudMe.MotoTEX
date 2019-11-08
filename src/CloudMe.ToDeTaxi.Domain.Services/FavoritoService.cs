using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class FavoritoService : ServiceBase<Favorito, FavoritoSummary, Guid>, IFavoritoService
    {
        private readonly IFavoritoRepository _FavoritoRepository;

        public FavoritoService(IFavoritoRepository FavoritoRepository)
        {
            _FavoritoRepository = FavoritoRepository;
        }

        public override string GetTag()
        {
            return "favorito";
        }

        protected override Task<Favorito> CreateEntryAsync(FavoritoSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Favorito = new Favorito
            {
                Id = summary.Id,
                IdPassageiro = summary.IdPassageiro,
                IdTaxista = summary.IdTaxista,
                Preferencia = summary.Preferencia
            };
            return Task.FromResult(Favorito);
        }

        protected override Task<FavoritoSummary> CreateSummaryAsync(Favorito entry)
        {
            var Favorito = new FavoritoSummary
            {
                Id = entry.Id,
                IdPassageiro = entry.IdPassageiro,
                IdTaxista = entry.IdTaxista,
                Preferencia = entry.Preferencia
            };

            return Task.FromResult(Favorito);
        }

        protected override Guid GetKeyFromSummary(FavoritoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Favorito> GetRepository()
        {
            return _FavoritoRepository;
        }

        protected override void UpdateEntry(Favorito entry, FavoritoSummary summary)
        {
            entry.IdPassageiro = summary.IdPassageiro;
            entry.IdTaxista = summary.IdTaxista;
            entry.Preferencia = summary.Preferencia;
        }

        protected override void ValidateSummary(FavoritoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Favorito: sumário é obrigatório"));
            }

            if (summary.IdPassageiro.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdPassageiro", "Favorito: passageiro inexistente ou não informado"));
            }

            if (summary.IdTaxista.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdTaxista", "Favorito: taxista inexistente ou não informado"));
            }
        }
    }
}
