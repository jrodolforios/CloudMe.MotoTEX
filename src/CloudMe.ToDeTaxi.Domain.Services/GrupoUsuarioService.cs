using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class GrupoUsuarioService : ServiceBase<GrupoUsuario, GrupoUsuarioSummary, Guid>, IGrupoUsuarioService
    {
        private readonly IGrupoUsuarioRepository _GrupoUsuarioRepository;

        public GrupoUsuarioService(IGrupoUsuarioRepository GrupoUsuarioRepository)
        {
            _GrupoUsuarioRepository = GrupoUsuarioRepository;
        }

        protected override Task<GrupoUsuario> CreateEntryAsync(GrupoUsuarioSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var GrupoUsuario = new GrupoUsuario
            {
                Id = summary.Id,
                Nome = summary.Nome,
                Descricao = summary.Descricao
            };
            return Task.FromResult(GrupoUsuario);
        }

        protected override Task<GrupoUsuarioSummary> CreateSummaryAsync(GrupoUsuario entry)
        {
            var GrupoUsuario = new GrupoUsuarioSummary
            {
                Id = entry.Id,
                Nome = entry.Nome,
                Descricao = entry.Descricao
            };

            return Task.FromResult(GrupoUsuario);
        }

        protected override Guid GetKeyFromSummary(GrupoUsuarioSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<GrupoUsuario> GetRepository()
        {
            return _GrupoUsuarioRepository;
        }

        protected override void UpdateEntry(GrupoUsuario entry, GrupoUsuarioSummary summary)
        {
            entry.Nome = summary.Nome;
            entry.Descricao = summary.Descricao;
        }

        protected override void ValidateSummary(GrupoUsuarioSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "GrupoUsuario: sumário é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.Nome))
            {
                this.AddNotification(new Notification("Nome", "GrupoUsuario: nome é obrigatório"));
            }
        }
    }
}
