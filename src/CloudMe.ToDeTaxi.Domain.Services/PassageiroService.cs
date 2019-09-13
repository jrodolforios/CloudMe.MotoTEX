using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using System.Linq;
using System.Linq.Expressions;
using CloudMe.ToDeTaxi.Domain.Model;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class PassageiroService : ServiceBase<Passageiro, PassageiroSummary, Guid>, IPassageiroService
    {
        private string[] defaultPaths = {"Endereco", "Usuario"};
        private readonly IPassageiroRepository _PassageiroRepository;

        public PassageiroService(IPassageiroRepository PassageiroRepository)
        {
            _PassageiroRepository = PassageiroRepository;
        }

        protected override Task<Passageiro> CreateEntryAsync(PassageiroSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Passageiro = new Passageiro
            {
                Id = summary.Id,
                IdUsuario = summary.IdUsuario,
                CPF = summary.CPF,
                IdEndereco = summary.Endereco.Id,
                IdLocalizacaoAtual = summary.IdLocalizacaoAtual,
                IdFoto = summary.IdFoto,
            };
            return Task.FromResult(Passageiro);
        }

        protected override Task<PassageiroSummary> CreateSummaryAsync(Passageiro entry)
        {
            var Passageiro = new PassageiroSummary
            {
                Id = entry.Id,
                IdUsuario = entry.IdUsuario,
                CPF = entry.CPF,
                IdFoto = entry.IdFoto,
                IdLocalizacaoAtual = entry.IdLocalizacaoAtual,
                Endereco = new LocalizacaoSummary()
                {
                    Id = entry.Endereco.Id,
                    Endereco = entry.Endereco.Endereco,
                    Longitude = entry.Endereco.Longitude,
                    Latitude = entry.Endereco.Latitude,
                    NomePublico = entry.Endereco.NomePublico
                },
            };

            return Task.FromResult(Passageiro);
        }

        protected override Guid GetKeyFromSummary(PassageiroSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Passageiro> GetRepository()
        {
            return _PassageiroRepository;
        }

        protected override void UpdateEntry(Passageiro entry, PassageiroSummary summary)
        {
            entry.IdUsuario = summary.IdUsuario;
            entry.CPF = summary.CPF;
            entry.IdEndereco = summary.Endereco.Id;
            entry.IdLocalizacaoAtual = summary.IdLocalizacaoAtual;
            entry.IdFoto = summary.IdFoto;
        }

        protected override void ValidateSummary(PassageiroSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Passageiro: sumário é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.CPF))
            {
                this.AddNotification(new Notification("CPF", "Passageiro: CPF é obrigatório"));
            }

            if (summary.IdUsuario.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdUsuario", "Passageiro: usuário inexistente ou não informado"));
            }
        }

        public override async Task<Passageiro> Get(Guid key, string[] paths = null)
        {
            return await base.Get(key, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override async Task<IEnumerable<Passageiro>> GetAll(string[] paths = null)
        {
            return await base.GetAll(paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override IEnumerable<Passageiro> Search(Expression<Func<Passageiro, bool>> where, string[] paths = null, SearchOptions options = null)
        {
            return base.Search(where, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths, options);
        }
    }
}
