using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Usuario;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class UsuarioGrupoUsuarioService : ServiceBase<UsuarioGrupoUsuario, UsuarioGrupoUsuarioSummary, Guid>, IUsuarioGrupoUsuarioService
    {
        private readonly IUsuarioGrupoUsuarioRepository _UsuarioGrupoUsuarioRepository;

        public UsuarioGrupoUsuarioService(IUsuarioGrupoUsuarioRepository UsuarioGrupoUsuarioRepository)
        {
            _UsuarioGrupoUsuarioRepository = UsuarioGrupoUsuarioRepository;
        }

        public override string GetTag()
        {
            return "usuario_grupo_usuario";
        }

        protected override Task<UsuarioGrupoUsuario> CreateEntryAsync(UsuarioGrupoUsuarioSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var UsuarioGrupoUsuario = new UsuarioGrupoUsuario
            {
                Id = summary.Id,
                IdUsuario = summary.IdUsuario,
                IdGrupoUsuario = summary.IdGrupoUsuario
            };
            return Task.FromResult(UsuarioGrupoUsuario);
        }

        protected override Task<UsuarioGrupoUsuarioSummary> CreateSummaryAsync(UsuarioGrupoUsuario entry)
        {
            var UsuarioGrupoUsuario = new UsuarioGrupoUsuarioSummary
            {
                Id = entry.Id,
                IdUsuario = entry.IdUsuario,
                IdGrupoUsuario = entry.IdGrupoUsuario
            };

            return Task.FromResult(UsuarioGrupoUsuario);
        }

        protected override Guid GetKeyFromSummary(UsuarioGrupoUsuarioSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<UsuarioGrupoUsuario> GetRepository()
        {
            return _UsuarioGrupoUsuarioRepository;
        }

        protected override void UpdateEntry(UsuarioGrupoUsuario entry, UsuarioGrupoUsuarioSummary summary)
        {
            entry.IdUsuario = summary.IdUsuario;
            entry.IdGrupoUsuario = summary.IdGrupoUsuario;
        }

        protected override void ValidateSummary(UsuarioGrupoUsuarioSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "UsuarioGrupoUsuario: sumário é obrigatório"));
            }

            if (summary.IdUsuario.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdUsuario", "UsuarioGrupoUsuario: usuário inexistente ou não informado"));
            }

            if (summary.IdGrupoUsuario.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdGrupoUsuario", "UsuarioGrupoUsuario: grupo de husuário inexistente ou não informado"));
            }
        }
    }
}
