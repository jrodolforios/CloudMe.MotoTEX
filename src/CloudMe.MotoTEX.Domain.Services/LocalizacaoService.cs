using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class LocalizacaoService : ServiceBase<Localizacao, LocalizacaoSummary, Guid>, ILocalizacaoService
    {
        private readonly ILocalizacaoRepository _LocalizacaoRepository;

        public LocalizacaoService(ILocalizacaoRepository LocalizacaoRepository)
        {
            _LocalizacaoRepository = LocalizacaoRepository;
        }

        public async Task<int> GetQtTaxistasOnline()
        {
            var result = await _LocalizacaoRepository.Search(
                x =>
                x.Usuario != null &&
                x.Usuario.tipo == Enums.TipoUsuario.Taxista &&
                x.Updated.Ticks >= (DateTime.Now.Ticks - 200000000)
                , new[] { "Usuario" });
            int quantidade = result.Count();

            return quantidade;
        }

        public override string GetTag()
        {
            return "localizacao";
        }

        protected override async Task<Localizacao> CreateEntryAsync(LocalizacaoSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new Localizacao
                {
                    Id = summary.Id,
                    Latitude = summary.Latitude,
                    Longitude = summary.Longitude,
                    Endereco = summary.Endereco,
                    NomePublico = summary.NomePublico,
                    IdUsuario = summary.IdUsuario
                };
            });
        }

        protected override async Task<LocalizacaoSummary> CreateSummaryAsync(Localizacao entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;

                return new LocalizacaoSummary
                {
                    Id = entry.Id,
                    Latitude = entry.Latitude,
                    Longitude = entry.Longitude,
                    Endereco = entry.Endereco,
                    NomePublico = entry.NomePublico,
                    IdUsuario = entry.IdUsuario
                };
            });
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
