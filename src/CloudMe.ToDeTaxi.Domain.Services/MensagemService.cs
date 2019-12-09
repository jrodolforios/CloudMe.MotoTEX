using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Domain.Model.Mensagem;
using CloudMe.ToDeTaxi.Domain.Model;
using System.Linq.Expressions;
using System.Linq;
using CloudMe.ToDeTaxi.Domain.Enums;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Domain.Notifications.Abstract;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class MensagemService : ServiceBase<Mensagem, MensagemSummary, Guid>, IMensagemService
    {
        private readonly IMensagemRepository mensagemRepository;
        private readonly IMensagemDestinatarioRepository mensagemDestinatarioRepository;
        private readonly IGrupoUsuarioRepository grupoUsuarioRepository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IProxyHubMensagens proxyMensagens;
        private readonly IUnitOfWork unitOfWork;

        public MensagemService(
            IMensagemRepository mensagemRepository,
            IMensagemDestinatarioRepository mensagemDestinatarioRepository,
            IGrupoUsuarioRepository grupoUsuarioService,
            IUsuarioRepository usuarioService,
            IProxyHubMensagens proxyMensagens,
            IUnitOfWork unitOfWork)
        {
            this.mensagemRepository = mensagemRepository;
            this.mensagemDestinatarioRepository = mensagemDestinatarioRepository;
            this.grupoUsuarioRepository = grupoUsuarioService;
            this.usuarioRepository = usuarioService;
            this.proxyMensagens = proxyMensagens;
            this.unitOfWork = unitOfWork;
        }

        public override string GetTag()
        {
            return "mensagem";
        }

        protected override Task<Mensagem> CreateEntryAsync(MensagemSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Mensagem = new Mensagem
            {
                Id = summary.Id,
                IdRemetente = summary.IdRemetente,
                Assunto = summary.Assunto,
                Corpo = summary.Corpo,
            };

            return Task.FromResult(Mensagem);
        }

        protected override Task<MensagemSummary> CreateSummaryAsync(Mensagem entry)
        {
            var Mensagem = new MensagemSummary
            {
                Id = entry.Id,
                IdRemetente = entry.IdRemetente,
                Assunto = entry.Assunto,
                Corpo = entry.Corpo
            };

            return Task.FromResult(Mensagem);
        }

        protected override Guid GetKeyFromSummary(MensagemSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Mensagem> GetRepository()
        {
            return mensagemRepository;
        }

        protected override void UpdateEntry(Mensagem entry, MensagemSummary summary)
        {
            entry.IdRemetente = summary.IdRemetente;
            entry.Assunto = summary.Assunto;
            entry.Corpo = summary.Corpo;
        }

        protected override void ValidateSummary(MensagemSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Mensagem: sumário é obrigatório"));
            }
        }

        public Task<IEnumerable<MensagemDestinatarioSummary>> ObterRecibosMensagem(Guid id_mensagem, Guid id_usuario)
        {
            var msgsDst = mensagemDestinatarioRepository.Search(
                x => x.IdMensagem == id_mensagem && x.IdUsuario == id_usuario);

            return Task.FromResult(msgsDst.Select(msgDst => new MensagemDestinatarioSummary()
            {
                Id = msgDst.Id,
                IdMensagem = msgDst.IdMensagem,
                IdUsuario = msgDst.IdUsuario,
                IdGrupoUsuario = msgDst.IdGrupoUsuario,
                DataLeitura = msgDst.DataLeitura,
                DataRecebimento = msgDst.DataRecebimento,
                Status = msgDst.Status
            }));
        }

        public async Task<bool> AlterarStatusMensagem(Guid id_mensagem, Guid id_usuario, StatusMensagem status)
        {
            var msgDst = mensagemDestinatarioRepository.Search(
                x => x.IdMensagem == id_mensagem && x.IdUsuario == id_usuario).FirstOrDefault();

            if (msgDst is null)
            {
                AddNotification(new Notification("Alterar status mensagem", "mensagem não encontrada ou não destinada ao usuário"));
                return false;
            }

            bool novoStatusValido = false;
            switch (msgDst.Status)
            {
                case StatusMensagem.Indefinido:
                    novoStatusValido = status == StatusMensagem.Enviada || status == StatusMensagem.Encaminhada;
                    break;
                case StatusMensagem.Enviada:
                case StatusMensagem.Encaminhada:
                    novoStatusValido = status == StatusMensagem.Recebida;
                    break;
                case StatusMensagem.Recebida:
                    novoStatusValido = status == StatusMensagem.Lida;
                    break;
                default:
                    break;
            }

            if (novoStatusValido)
            {
                msgDst.Status = status;
                return await mensagemDestinatarioRepository.ModifyAsync(msgDst);
            }

            return false;
        }

        private DetalhesMensagem detalharMensagem(Mensagem msg)
        {
            return new DetalhesMensagem()
            {
                IdMensagem = msg.Id,
                IdRemetente = msg.IdRemetente,
                destinatarios = new DestinatariosMensagem()
                {
                    IdsUsuarios = msg.Destinatarios
                        .Where(msgUsr => !msgUsr.IdGrupoUsuario.HasValue)
                        .Select(msgUsr => msgUsr.IdUsuario)
                        .Distinct(),

                    IdsGruposUsuarios = msg.Destinatarios
                        .Where(msgUsr => msgUsr.IdGrupoUsuario.HasValue)
                        .Select(msgUsr => msgUsr.IdGrupoUsuario.Value)
                        .Distinct()
                },
                Assunto = msg.Assunto,
                Corpo = msg.Corpo,
                DataEnvio = msg.Inserted,
                DataLeitura =
                    msg.Destinatarios.All(msgUsr => msgUsr.DataLeitura.HasValue) ?
                    msg.Destinatarios.Max(msgUsr => msgUsr.DataLeitura) : null,
                DataRecebimento =
                    msg.Destinatarios.All(msgUsr => msgUsr.DataRecebimento.HasValue) ?
                    msg.Destinatarios.Max(msgUsr => msgUsr.DataRecebimento) : null
            };
        }

        #region ENVIO
        private async Task<int> EnviarOuEncaminhar(MensagemSummary mensagem, DestinatariosMensagem destinatarios, bool encaminhar)
        {
            int usrSentCount = 0;

            var usuarios = usuarioRepository.FindAll()
                .Where(x => destinatarios.IdsUsuarios.Contains(x.Id));

            var grupos = grupoUsuarioRepository.FindAll(new[] {"Usuarios"})
                .Where(x => destinatarios.IdsGruposUsuarios.Contains(x.Id));

            var idsGruposEnviados = new List<Guid>();

            // cria a mensagem
            mensagem.Id = Guid.Empty;
            var msg = await CreateAsync(mensagem);
            if (IsInvalid())
            {
                return 0;
            }

            // envia para usuários
            if (usuarios.Count() > 0)
            {
                foreach (var usuario in usuarios)
                {
                    // associa ao destinatário
                    var msgDest = new MensagemDestinatario
                    {
                        IdMensagem = msg.Id,
                        IdUsuario = usuario.Id,
                        Status = encaminhar ? StatusMensagem.Encaminhada : StatusMensagem.Enviada
                    };

                    await mensagemDestinatarioRepository.SaveAsync(msgDest);
                    usrSentCount++;
                }
            }

            // envia para os grupos
            if(grupos.Count() > 0)
            {
                foreach (var grupo in grupos)
                {
                    foreach (var usuario in grupo.Usuarios)
                    {
                        // associa ao destinatário
                        var msgDest = new MensagemDestinatario
                        {
                            IdMensagem = msg.Id,
                            IdUsuario = usuario.IdUsuario,
                            IdGrupoUsuario = grupo.Id,
                            Status = encaminhar ? StatusMensagem.Encaminhada : StatusMensagem.Enviada
                        };

                        await mensagemDestinatarioRepository.SaveAsync(msgDest);
                        usrSentCount++;
                    }
                }
            }

            await unitOfWork.CommitAsync();

            var detalhes = detalharMensagem(msg);

            // notifica usuários
            await proxyMensagens.EnviarParaUsuarios(usuarios, detalhes);

            // notifica grupos
            foreach (var grupo in grupos)
            {
                await proxyMensagens.EnviarParaGrupoUsuarios(grupo, detalhes);
            }

            return usrSentCount;
        }

        public async Task<int> Enviar(MensagemSummary mensagem, DestinatariosMensagem destinatarios)
        {
            return await EnviarOuEncaminhar(mensagem, destinatarios, false);
        }

        public async Task<int> Encaminhar(Guid id_mensagem, DestinatariosMensagem destinatarios)
        {
            var msg = await mensagemRepository.FindByIdAsync(id_mensagem);
            if (msg == null)
            {
                AddNotification(new Notification("Encaminhar mensagem", "mensagem não encontrada"));
                return 0;
            }

            return await EnviarOuEncaminhar(await CreateSummaryAsync(msg), destinatarios, true);
        }

        #endregion

        #region RECEBIMENTO

        public Task<IEnumerable<DetalhesMensagem>> ObterMensagensEnviadas(Guid id_usuario, DateTime? inicio, DateTime? fim, Pagination pagination, out int count)
        {
            DateTime dataInicio = inicio ?? DateTime.MinValue;
            DateTime dataFim = fim ?? DateTime.MaxValue;

            var msgs = mensagemRepository.Search(x =>
                    x.IdRemetente == id_usuario &&
                    x.Inserted >= dataInicio &&
                    x.Inserted <= dataFim,
                    new[] { "Destinatarios" });

            count = msgs.Count();

            msgs = msgs.OrderByDescending(x => x.Inserted)
                .Skip(pagination.page * pagination.itensPerPage)
                .Take(pagination.itensPerPage);


            return Task.FromResult(msgs.Select(msg => detalharMensagem(msg)));
        }

        public Task<IEnumerable<DetalhesMensagem>> ObterMensagensRecebidas(Guid id_usuario, DateTime? inicio, DateTime? fim, Pagination pagination, out int count)
        {
            DateTime dataInicio = inicio ?? DateTime.MinValue;
            DateTime dataFim = fim ?? DateTime.MaxValue;

            var msgs = mensagemRepository.Search(x =>
                    x.Destinatarios.Any(dest => dest.IdUsuario == id_usuario) &&
                    x.Inserted >= dataInicio &&
                    x.Inserted <= dataFim,
                    new[] { "Destinatarios" }).Distinct();

            count = msgs.Count();

            msgs = msgs.OrderByDescending(x => x.Inserted)
                .Skip(pagination.page * pagination.itensPerPage)
                .Take(pagination.itensPerPage);

            return Task.FromResult(msgs.Select(msg => detalharMensagem(msg)));
        }

        #endregion

    }
}
