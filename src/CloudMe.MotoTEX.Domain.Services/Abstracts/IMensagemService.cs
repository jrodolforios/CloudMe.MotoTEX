using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Model.Mensagem;
using CloudMe.MotoTEX.Domain.Enums;
using CloudMe.MotoTEX.Domain.Model;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IMensagemService : IServiceBase<Mensagem, MensagemSummary, Guid>
    {
        Task<Tuple<IEnumerable<DetalhesMensagem>, int>> ObterMensagensEnviadas(Guid id_usuario, DateTime? inicio, DateTime? fim, Pagination pagination);
        Task<Tuple<IEnumerable<DetalhesMensagem>, int>> ObterMensagensRecebidasAsync(Guid id_usuario, DateTime? inicio, DateTime? fim, Pagination pagination);

        Task<int> Enviar(MensagemSummary mensagem, DestinatariosMensagem destinatarios);
        Task<int> Encaminhar(Guid id_mensagem, DestinatariosMensagem destinatarios);

        Task<IEnumerable<MensagemDestinatarioSummary>> ObterRecibosMensagem(Guid id_mensagem, Guid id_usuario);
        Task<bool> AlterarStatusMensagem(Guid id_mensagem, Guid id_usuario, StatusMensagem status);
        Task<List<DetalhesMensagem>> ObterMensagensEnviadasEMarcarLidas(Guid id_usuario);
    }
}
