using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Domain.Model.Mensagem;
using CloudMe.ToDeTaxi.Domain.Enums;
using CloudMe.ToDeTaxi.Domain.Model;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IMensagemService : IServiceBase<Mensagem, MensagemSummary, Guid>
    {
        Task<IEnumerable<DetalhesMensagem>> ObterMensagensEnviadas(Guid id_usuario, DateTime? inicio, DateTime? fim, Pagination pagination, out int count);
        Task<IEnumerable<DetalhesMensagem>> ObterMensagensRecebidas(Guid id_usuario, DateTime? inicio, DateTime? fim, Pagination pagination, out int count);

        Task<int> Enviar(MensagemSummary mensagem, DestinatariosMensagem destinatarios);
        Task<int> Encaminhar(Guid id_mensagem, DestinatariosMensagem destinatarios);

        Task<IEnumerable<MensagemDestinatarioSummary>> ObterRecibosMensagem(Guid id_mensagem, Guid id_usuario);
        Task<bool> AlterarStatusMensagem(Guid id_mensagem, Guid id_usuario, StatusMensagem status);
        Task<List<DetalhesMensagem>> ObterMensagensEnviadasEMarcarLidas(Guid id_usuario);
    }
}
