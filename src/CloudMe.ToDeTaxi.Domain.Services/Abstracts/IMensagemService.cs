using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Domain.Model.Mensagem;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IMensagemService : IServiceBase<Mensagem, MensagemSummary, Guid>
    {
        Task<IEnumerable<Guid>> ObterConversacoesComUsuarios(Guid id_usuario, DateTime? inicio, DateTime? fim);
        Task<IEnumerable<Guid>> ObterConversacoesComGruposUsuarios(Guid id_usuario, DateTime? inicio, DateTime? fim);

        Task<IEnumerable<DetalhesMensagem>> ObterMensagensConversacaoUsuario(Guid id_usuario, Guid id_usuario_conversacao, DateTime? inicio, DateTime? fim);
        Task<IEnumerable<DetalhesMensagem>> ObterMensagensConversacaoGrupoUsuario(Guid id_grupo_usuario, DateTime? inicio, DateTime? fim);

        Task<bool> EnviarParaUsuario(Guid id_usuario, MensagemSummary mensagem);
        Task<int> EnviarParaUsuarios(IEnumerable<Guid> ids_usuarios, MensagemSummary mensagem);
        Task<int> EnviarParaGrupoUsuarios(Guid id_grupo_usuario, MensagemSummary mensagem);

        Task<bool> AlterarStatusMensagem(Guid id_mensagem, Guid id_usuario, StatusMensagem status);
    }
}
