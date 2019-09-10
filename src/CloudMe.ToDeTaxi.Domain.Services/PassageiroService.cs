using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class PassageiroService : ServiceBase<Passageiro, PassageiroSummary, Guid>, IPassageiroService
    {
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
                IdEndereco = summary.IdEndereco,
                IdLocalizacaoAtual = summary.IdLocalizacaoAtual,
                IdFoto = summary.IdFoto,
                CPF = summary.CPF
            };
            return Task.FromResult(Passageiro);
        }

        protected override Task<PassageiroSummary> CreateSummaryAsync(Passageiro entry)
        {
            var Passageiro = new PassageiroSummary
            {
                Id = entry.Id,
                IdUsuario = entry.IdUsuario,
                IdEndereco = entry.IdEndereco,
                IdLocalizacaoAtual = entry.IdLocalizacaoAtual,
                IdFoto = entry.IdFoto,
                CPF = entry.CPF
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
            entry.IdEndereco = summary.IdEndereco;
            entry.IdLocalizacaoAtual = summary.IdLocalizacaoAtual;
            entry.IdFoto = summary.IdFoto;
            entry.CPF = summary.CPF;
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

            if (summary.IdEndereco.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdEndereco", "Passageiro: endereço inexistente ou não informado"));
            }
        }
    }
}
