using CloudMe.MotoTEX.Domain.Model.Taxista;
using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;
using CloudMe.MotoTEX.Domain.Notifications.Compat;
using CloudMe.MotoTEX.Domain.Notifications.Hubs;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Proxies
{
    public class ProxyLocalizacao : IProxyLocalizacao
    {
        IHubContext<HubNotificacoes> hubNotificacoes;
        IHubContext<HubNotificaoesAdmin> hubNotificacoesAdmin;
        IUsuarioRepository usuarioRepository;

        IHubContext<HubLocalizacaoTaxista> hubLocTaxista; // COMPAT
        IHubContext<HubLocalizacaoPassageiro> hubLocPassageiro; // COMPAT

        public ProxyLocalizacao(
            IHubContext<HubNotificacoes> hubNotificacoes,
            IHubContext<HubNotificaoesAdmin> hubNotificacoesAdmin,
            IUsuarioRepository usuarioRepository,
            IHubContext<HubLocalizacaoTaxista> hubLocTaxista, // COMPAT
            IHubContext<HubLocalizacaoPassageiro> hubLocPassageiro) // COMPAT
        {
            this.hubNotificacoes = hubNotificacoes;
            this.hubNotificacoesAdmin = hubNotificacoesAdmin;
            this.usuarioRepository = usuarioRepository;

            this.hubLocTaxista = hubLocTaxista; // COMPAT
            this.hubLocPassageiro = hubLocPassageiro; // COMPAT
        }

        public async Task SolicitarLocalizacaoTaxistas()
        {
            var idsUsrTaxistas = (await usuarioRepository.Search(x => x.tipo == Enums.TipoUsuario.Taxista))
                .Select(x => x.Id.ToString())
                .ToArray();

            await hubNotificacoes.Clients.Users(idsUsrTaxistas).SendAsync("get_loc_tx");
            await hubLocTaxista.Clients.All.SendAsync("EnviarLocalizacao"); // COMPAT
        }

        public async Task SolicitarLocalizacaoPassageiros()
        {
            var idsUsrPassageiros = (await usuarioRepository.Search(x => x.tipo == Enums.TipoUsuario.Passageiro))
                .Select(x => x.Id.ToString())
                .ToArray();

            await hubNotificacoes.Clients.Users(idsUsrPassageiros).SendAsync("get_loc_psg");
            await hubLocPassageiro.Clients.All.SendAsync("EnviarLocalizacao"); // COMPAT
        }

        public async Task InformarLocalizacaoTaxista(Guid idTaxista, double latitude, double longitude, double angulo)
        {
            await hubNotificacoesAdmin.Clients.All.SendAsync("loc_tx", new
            {
                id = idTaxista,
                lat = latitude,
                lgt = longitude,
                angulo
            });
        }

        public async Task InformarLocalizacaoPassageiro(Guid idPassageiro, double latitude, double longitude, double angulo)
        {
            await hubNotificacoesAdmin.Clients.All.SendAsync("loc_pass", new
            {
                id = idPassageiro,
                lat = latitude,
                lgt = longitude,
                angulo
            });

        }
    }
}
