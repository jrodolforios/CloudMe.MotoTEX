using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class TaxistaService : ServiceBase<Taxista, TaxistaSummary, Guid>, ITaxistaService
    {
        private string[] defaultPaths = {"Endereco", "Usuario"};

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
                RG = summary.RG,
                CPF = summary.CPF,
                IdFoto = summary.IdFoto,
                IdLocalizacaoAtual = summary.IdLocalizacaoAtual,
                IdPontoTaxi = summary.IdPontoTaxi,
                IdEndereco = summary.Endereco.Id,
            };
            return Task.FromResult(Taxista);
        }

        protected override Task<TaxistaSummary> CreateSummaryAsync(Taxista entry)
        {
            var Taxista = new TaxistaSummary
            {
                Id = entry.Id,
                IdUsuario = entry.IdUsuario,
                RG = entry.RG,
                CPF = entry.CPF,
                IdFoto = entry.IdFoto,
                IdLocalizacaoAtual = entry.IdLocalizacaoAtual,
                IdPontoTaxi = entry.IdPontoTaxi,
                Endereco = new LocalizacaoSummary()
                {
                    Id = entry.Endereco.Id,
                    Endereco = entry.Endereco.Endereco,
                    Longitude = entry.Endereco.Longitude,
                    Latitude = entry.Endereco.Latitude,
                    NomePublico = entry.Endereco.NomePublico
                },
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
            entry.RG = summary.RG;
            entry.CPF = summary.CPF;
            entry.IdFoto = summary.IdFoto;
            entry.IdLocalizacaoAtual = summary.IdLocalizacaoAtual;
            entry.IdPontoTaxi = summary.IdPontoTaxi;
            entry.IdEndereco = summary.Endereco.Id;
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

            if (string.IsNullOrEmpty(summary.RG))
            {
                this.AddNotification(new Notification("RG", "Passageiro: RG é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.CPF))
            {
                this.AddNotification(new Notification("CPF", "Passageiro: CPF é obrigatório"));
            }
        }

        public override async Task<Taxista> Get(Guid key, string[] paths = null)
        {
            return await base.Get(key, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override async Task<IEnumerable<Taxista>> GetAll(string[] paths = null)
        {
            return await base.GetAll(paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override IEnumerable<Taxista> Search(Expression<Func<Taxista, bool>> where, string[] paths = null, SearchOptions options = null)
        {
            return base.Search(where, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths, options);
        }
    }
}
