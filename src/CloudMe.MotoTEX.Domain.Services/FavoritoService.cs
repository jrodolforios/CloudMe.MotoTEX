using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Passageiro;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services
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

        protected override async Task<Favorito> CreateEntryAsync(FavoritoSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new Favorito
                {
                    Id = summary.Id,
                    IdPassageiro = summary.IdPassageiro,
                    IdTaxista = summary.IdTaxista,
                    Preferencia = summary.Preferencia
                };
            });
        }

        protected override async Task<FavoritoSummary> CreateSummaryAsync(Favorito entry)
        {
            if (entry == null) return default;

            return await Task.Run(() =>
            {
                return new FavoritoSummary
                {
                    Id = entry.Id,
                    IdPassageiro = entry.IdPassageiro,
                    IdTaxista = entry.IdTaxista,
                    Preferencia = entry.Preferencia
                };
            });
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
