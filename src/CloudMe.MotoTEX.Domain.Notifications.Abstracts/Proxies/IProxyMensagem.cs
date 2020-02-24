using CloudMe.MotoTEX.Domain.Model.Mensagem;
using CloudMe.MotoTEX.Infraestructure.Entries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies
{
    public interface IProxyMensagem
    {
        Task EnviarParaUsuario(Usuario usuario, DetalhesMensagem mensagem);
        Task EnviarParaUsuarios(IEnumerable<Usuario> usuarios, DetalhesMensagem mensagem);
        Task EnviarParaGrupoUsuarios(GrupoUsuario grupoUsuario, DetalhesMensagem mensagem);
        Task MensagemAtualizada(MensagemDestinatarioSummary mensagemDestinatario);
    }
}
