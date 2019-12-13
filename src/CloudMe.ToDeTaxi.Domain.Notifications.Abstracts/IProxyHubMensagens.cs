using CloudMe.ToDeTaxi.Domain.Model.Mensagem;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Notifications.Abstract
{
    public interface IProxyHubMensagens
    {
        Task EnviarParaUsuario(Usuario usuario, DetalhesMensagem mensagem);
        Task EnviarParaUsuarios(IEnumerable<Usuario> usuarios, DetalhesMensagem mensagem);
        Task EnviarParaGrupoUsuarios(GrupoUsuario grupoUsuario, DetalhesMensagem mensagem);
        Task MensagemAtualizada(MensagemDestinatarioSummary mensagemDestinatario);
    }
}
