using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class LocalizacaoService : ServiceBase<Localizacao, LocalizacaoSummary, Guid>, ILocalizacaoService
    {
        private readonly ILocalizacaoRepository _LocalizacaoRepository;

        public LocalizacaoService(ILocalizacaoRepository LocalizacaoRepository)
        {
            _LocalizacaoRepository = LocalizacaoRepository;
        }

        public override string GetTag()
        {
            return "localizacao";
        }

        protected override Task<Localizacao> CreateEntryAsync(LocalizacaoSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Localizacao = new Localizacao
            {
                Id = summary.Id,
                Latitude = summary.Latitude,
                Longitude = summary.Longitude,
                Endereco = summary.Endereco,
                NomePublico = summary.NomePublico,
                IdUsuario = summary.IdUsuario
            };
            return Task.FromResult(Localizacao);
        }

        protected override Task<LocalizacaoSummary> CreateSummaryAsync(Localizacao entry)
        {
            var Localizacao = new LocalizacaoSummary
            {
                Id = entry.Id,
                Latitude = entry.Latitude,
                Longitude = entry.Longitude,
                Endereco = entry.Endereco,
                NomePublico = entry.NomePublico,
                IdUsuario = entry.IdUsuario
            };

            return Task.FromResult(Localizacao);
        }

        protected override Guid GetKeyFromSummary(LocalizacaoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Localizacao> GetRepository()
        {
            return _LocalizacaoRepository;
        }

        protected override void UpdateEntry(Localizacao entry, LocalizacaoSummary summary)
        {
            entry.Latitude = summary.Latitude;
            entry.Longitude = summary.Longitude;
            entry.Endereco = summary.Endereco;
            entry.NomePublico = summary.NomePublico;
            entry.IdUsuario = summary.IdUsuario;
        }

        protected override void ValidateSummary(LocalizacaoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Localizacao: sumário é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.Longitude))
            {
                this.AddNotification(new Notification("Longitude", "Localizacao: longitude é obrigatória"));
            }

            if (string.IsNullOrEmpty(summary.Latitude))
            {
                this.AddNotification(new Notification("Latitude", "Localizacao: latitude é obrigatória"));
            }
        }
    }
}
