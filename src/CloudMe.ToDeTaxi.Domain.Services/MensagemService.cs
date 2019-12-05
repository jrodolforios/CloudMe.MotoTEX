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
using Microsoft.AspNetCore.SignalR;
using CloudMe.ToDeTaxi.Domain.Notifications.Abstracts.Hubs;
using CloudMe.ToDeTaxi.Domain.Enums;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class MensagemService : ServiceBase<Mensagem, MensagemSummary, Guid>, IMensagemService
    {
        private readonly IMensagemRepository mensagemRepository;
        private readonly IMensagemDestinatarioRepository mensagemDestinatarioRepository;
        private readonly IGrupoUsuarioService grupoUsuarioService;
        private readonly IUsuarioService usuarioService;
        private readonly IHubMensagens hubMensagens;
        private readonly IUnitOfWork unitOfWork;

        public MensagemService(
            IMensagemRepository mensagemRepository,
            IMensagemDestinatarioRepository mensagemDestinatarioRepository,
            IGrupoUsuarioService grupoUsuarioService,
            IUsuarioService usuarioService,
            IHubMensagens hubMensagens,
            IUnitOfWork unitOfWork)
        {
            this.mensagemRepository = mensagemRepository;
            this.mensagemDestinatarioRepository = mensagemDestinatarioRepository;
            this.grupoUsuarioService = grupoUsuarioService;
            this.usuarioService = usuarioService;
            this.hubMensagens = hubMensagens;
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

        public async Task<bool> AlterarStatusMensagem(Guid id_mensagem, Guid id_usuario, StatusMensagem status)
        {
            var mstDst = mensagemDestinatarioRepository.Search(
                x => x.IdMensagem == id_mensagem && x.IdUsuario == id_usuario).FirstOrDefault();

            if (mstDst is null)
            {
                AddNotification(new Notification("Alterar status mensagem", "mensagem não encontrada ou não destinada ao usuário"));
                return false;
            }

            bool novoStatusValido = false;
            switch (mstDst.Status)
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
                mstDst.Status = status;
                return await mensagemDestinatarioRepository.ModifyAsync(mstDst);
            }

            return false;
        }

        public async Task<bool> EnviarParaUsuario(Guid id_usuario, MensagemSummary mensagem)
        {
            // obtém o usuário
            var usr = await usuarioService.Get(id_usuario);
            if (usr == null)
            {
                AddNotification(
                    new Notification(
                        "Enviar mensagem", 
                        string.Format("Mensagem não enviada: usuário {0} não encontrado.", id_usuario.ToString())
                        )
                    );
                return false;
            }

            // cria a mensagem
            var msg = await CreateAsync(mensagem);
            if (IsInvalid())
            {
                return false;
            }

            // associa ao destinatário
            var msgDest = new MensagemDestinatario
            {
                IdMensagem = msg.Id,
                IdUsuario = usr.Id,
                Status = StatusMensagem.Enviada
            };

            await mensagemDestinatarioRepository.SaveAsync(msgDest);
            await unitOfWork.CommitAsync();

            await hubMensagens.EnviarParaUsuario(usr, new DetalhesMensagem()
            {
                IdMensagem = msg.Id,
                IdRemetente = msg.IdRemetente,
                IdDestinatario = msgDest.IdUsuario,
                Assunto = msg.Assunto,
                Corpo = msg.Corpo,
                DataEnvio = msg.Inserted
            });

            return true;
        }

        public async Task<int> EnviarParaUsuarios(IEnumerable<Guid> ids_usuarios, MensagemSummary mensagem)
        {
            var usuarios = new List<Usuario>();
            foreach (var id_usuario in ids_usuarios)
            {
                var usuario = await usuarioService.Get(id_usuario);
                if (usuario != null)
                {
                    usuarios.Add(usuario);
                }
            }

            int usrSentCount = 0;
            if (usuarios.Count() > 0)
            {
                // cria a mensagem
                var msg = await CreateAsync(mensagem);
                if (IsInvalid())
                {
                    return 0;
                }

                foreach (var usuario in usuarios)
                {
                    if (usuario.Id == mensagem.IdRemetente)
                    {
                        // não manda pra o próprio remetente
                        continue;
                    }

                    // associa ao destinatário
                    var msgDest = new MensagemDestinatario
                    {
                        IdMensagem = msg.Id,
                        IdUsuario = usuario.Id,
                        Status = StatusMensagem.Enviada
                    };

                    await mensagemDestinatarioRepository.SaveAsync(msgDest);

                    usrSentCount++;
                }

                await unitOfWork.CommitAsync();

                await hubMensagens.EnviarParaUsuarios(usuarios, new DetalhesMensagem()
                {
                    IdMensagem = msg.Id,
                    IdRemetente = msg.IdRemetente,
                    Assunto = msg.Assunto,
                    Corpo = msg.Corpo,
                    DataEnvio = msg.Inserted
                });
            }

            return usrSentCount;
        }

        public async Task<int> EnviarParaGrupoUsuarios(Guid id_grupo_usuario, MensagemSummary mensagem)
        {
            // obtém o grupo de usuários
            var grupoUsr = await grupoUsuarioService.Get(id_grupo_usuario, new[] { "Usuarios" });
            if (grupoUsr == null)
            {
                AddNotification(
                    new Notification(
                        "Enviar mensagem",
                        string.Format("Mensagem não enviada: grupo de usuários {0} não encontrado.", id_grupo_usuario.ToString())
                    )
                );
                return 0;
            }

            // cria a mensagem
            var msg = await CreateAsync(mensagem);
            if (IsInvalid())
            {
                return 0;
            }

            /*
            // envia ao destinatário
            var msgDest = new MensagemDestinatario
            {
                IdMensagem = msg.Id,
                IdGrupoUsuario = grupoUsr.Id
                //Status = Enums.StatusMensagem.Enviada TODO
            };
            */

            int usrSentCount = 0;

            foreach (var usuario in grupoUsr.Usuarios)
            {
                if (usuario.Id == mensagem.IdRemetente)
                {
                    // o usuário é o remetente e também participa do grupo em questão (não manda pra ele mesmo)
                    continue;
                }

                // associa ao destinatário
                var msgDest = new MensagemDestinatario
                {
                    IdMensagem = msg.Id,
                    IdUsuario = usuario.IdUsuario,
                    IdGrupoUsuario = grupoUsr.Id,
                    Status = StatusMensagem.Enviada
                };

                await mensagemDestinatarioRepository.SaveAsync(msgDest);

                usrSentCount++;
            }

            await unitOfWork.CommitAsync();

            await hubMensagens.EnviarParaGrupoUsuarios(grupoUsr, new DetalhesMensagem()
            {
                IdMensagem = msg.Id,
                IdRemetente = msg.IdRemetente,
                Assunto = msg.Assunto,
                Corpo = msg.Corpo,
                DataEnvio = msg.Inserted
            });

            return usrSentCount;
        }

        public async Task<IEnumerable<Guid>> ObterConversacoesComUsuarios(Guid id_usuario, DateTime? inicio, DateTime? fim)
        {
            return mensagemDestinatarioRepository.Search( x =>
                    (x.Mensagem.IdRemetente == id_usuario || x.Usuario.Id == id_usuario) &&
                    (!x.IdGrupoUsuario.HasValue), new[] { "Mensagem" })
                    .Select(x => x.Id)
                    .Distinct();
        }

        public async Task<IEnumerable<Guid>> ObterConversacoesComGruposUsuarios(Guid id_usuario, DateTime? inicio, DateTime? fim)
        {
            return mensagemDestinatarioRepository.Search(x =>
                   (x.Mensagem.IdRemetente == id_usuario || x.Usuario.Id == id_usuario) &&
                   (x.IdGrupoUsuario.HasValue), new[] { "Mensagem" })
                    .Select(x => x.IdGrupoUsuario.Value)
                    .Distinct();
        }

        private IEnumerable<DetalhesMensagem> DetalharMensagens(IEnumerable<MensagemDestinatario> mensagensDestinatarios)
        {
            return mensagensDestinatarios.Select(x => new DetalhesMensagem()
            {
                IdMensagem = x.Mensagem.Id,
                IdRemetente = x.Mensagem.IdRemetente,
                IdDestinatario = x.IdUsuario,
                IdGrupo = x.IdGrupoUsuario,
                Assunto = x.Mensagem.Assunto,
                Corpo = x.Mensagem.Corpo,
                DataEnvio = x.Mensagem.Inserted,
                DataLeitura = x.DataLeitura,
                DataRecebimento = x.DataRecebimento
            });
        }

        public async Task<IEnumerable<DetalhesMensagem>> ObterMensagensConversacaoUsuario(Guid id_usuario, Guid id_usuario_conversacao, DateTime? inicio, DateTime? fim)
        {
            /*var msgsDest = mensagemDestinatarioRepository.Search(x =>
                    ((x.Mensagem.IdRemetente == id_usuario && x.IdUsuario == id_usuario_conversacao) ||
                     (x.Mensagem.IdRemetente == id_usuario_conversacao && x.IdUsuario == id_usuario)) &&
                    x.Mensagem.Inserted >= inicio &&
                    x.Mensagem.Inserted <= fim, 
                    new[] { "Mensagem" })
                    .OrderBy(x => x.Mensagem.Inserted)
                    .Distinct();

            return DetalharMensagens(msgsDest);*/

            var msgs = mensagemRepository.Search(x =>
                    (( x.IdRemetente == id_usuario && x.Destinatarios.Any(dest => dest.IdUsuario == id_usuario_conversacao) ) ||
                     ( x.IdRemetente == id_usuario_conversacao && x.Destinatarios.Any(dest => dest.IdUsuario == id_usuario) )) &&
                    x.Inserted >= inicio &&
                    x.Inserted <= fim,
                    new[] { "Destinatarios" })
                    .OrderBy(x => x.Inserted)
                    .Distinct();

            return msgs.Select(msg =>
            {
                var msgDst = msg.Destinatarios.FirstOrDefault(x => 
                    x.IdUsuario == id_usuario_conversacao || 
                    x.IdUsuario == id_usuario);

                return new DetalhesMensagem()
                {
                    IdMensagem = msg.Id,
                    IdRemetente = msg.IdRemetente,
                    IdDestinatario = msgDst.IdUsuario,
                    IdGrupo = msgDst.IdGrupoUsuario,
                    Assunto = msg.Assunto,
                    Corpo = msg.Corpo,
                    DataEnvio = msg.Inserted,
                    DataRecebimento = msgDst.DataRecebimento,
                    DataLeitura = msgDst.DataLeitura
                };
            });

        }

        public async Task<IEnumerable<DetalhesMensagem>> ObterMensagensConversacaoGrupoUsuario(Guid id_grupo_usuario, DateTime? inicio, DateTime? fim)
        {
            /*var msgsDest = mensagemDestinatarioRepository.Search(x =>
                    (x.IdGrupoUsuario.HasValue && x.IdGrupoUsuario.Value == id_grupo_usuario) &&
                    x.Mensagem.Inserted >= inicio &&
                    x.Mensagem.Inserted <= fim,
                    new[] { "Mensagem" }).OrderBy(x => x.Mensagem.Inserted);

            return DetalharMensagens(msgsDest);*/
            var msgs = mensagemRepository.Search(x =>
                    (x.Destinatarios.Any(dest => dest.IdGrupoUsuario.HasValue && dest.IdGrupoUsuario.Value == id_grupo_usuario)) &&
                    x.Inserted >= inicio &&
                    x.Inserted <= fim,
                    new[] { "Destinatarios" })
                    .OrderBy(x => x.Inserted);

            return msgs.Select(msg =>
            {
                var msgDst = msg.Destinatarios.FirstOrDefault(dest =>
                    dest.IdGrupoUsuario.HasValue && dest.IdGrupoUsuario.Value == id_grupo_usuario);

                return new DetalhesMensagem()
                {
                    IdMensagem = msg.Id,
                    IdRemetente = msg.IdRemetente,
                    IdDestinatario = msgDst.IdUsuario,
                    IdGrupo = msgDst.IdGrupoUsuario,
                    Assunto = msg.Assunto,
                    Corpo = msg.Corpo,
                    DataEnvio = msg.Inserted,
                    DataRecebimento = msgDst.DataRecebimento,
                    DataLeitura = msgDst.DataLeitura
                };
            });


        }

    }
}
