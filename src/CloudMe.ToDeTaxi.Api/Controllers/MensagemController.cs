using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;
using CloudMe.ToDeTaxi.Domain.Enums;
using CloudMe.ToDeTaxi.Domain.Model.Mensagem;
using CloudMe.ToDeTaxi.Api.Models.Mensagens;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class MensagemController : BaseController
    {
        IMensagemService _MensagemService;

        public MensagemController(IMensagemService MensagemService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _MensagemService = MensagemService;
        }

        /// <summary>
        /// Obtém as conversações de um usuário com outros usuários (retorna o ID de cada usuário).
        /// </summary>
        [HttpGet("conversacoes_usrs")]
        [ProducesResponseType(typeof(Response<IEnumerable<Guid>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<Guid>>> ObterConversacoesComUsuarios(Guid id_usuario, DateTime? inicio, DateTime? fim)
        {
            return await base.ResponseAsync(
                await _MensagemService.ObterConversacoesComUsuarios(id_usuario, inicio, fim), _MensagemService);
        }

        /// <summary>
        /// Obtém as conversações de um usuário com grupos de usuários (retorna o ID de cada grupo).
        /// </summary>
        [HttpGet("conversacoes_grps_usrs")]
        [ProducesResponseType(typeof(Response<IEnumerable<Guid>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<Guid>>> ObterConversacoesComGruposUsuarios(Guid id_usuario, DateTime? inicio, DateTime? fim)
        {
            return await base.ResponseAsync(
                await _MensagemService.ObterConversacoesComGruposUsuarios(id_usuario, inicio, fim), _MensagemService);
        }

        /// <summary>
        /// Obtém mensagens da conversação de um usuário com outro usuário.
        /// </summary>
        [HttpGet("msgs_conversacao_usr")]
        [ProducesResponseType(typeof(Response<IEnumerable<DetalhesMensagem>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<DetalhesMensagem>>> ObterMensagensConversacaoUsuario(Guid id_usuario, Guid id_usuario_conversacao, DateTime? inicio, DateTime? fim)
        {
            return await base.ResponseAsync(
                await _MensagemService.ObterMensagensConversacaoUsuario(id_usuario, id_usuario_conversacao, inicio, fim), _MensagemService);
        }

        /// <summary>
        /// Obtém mensagens da conversação em um grupo de usuários.
        /// </summary>
        [HttpGet("msgs_conversacao_grp_usr")]
        [ProducesResponseType(typeof(Response<IEnumerable<Guid>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<DetalhesMensagem>>> ObterMensagensConversacaoGrupoUsuario(Guid id_grupo_usuario, DateTime? inicio, DateTime? fim)
        {
            return await base.ResponseAsync(
                await _MensagemService.ObterMensagensConversacaoGrupoUsuario(id_grupo_usuario, inicio, fim), _MensagemService);
        }

        /// <summary>
        /// Envia uma mensagem para um usuário.
        /// </summary>
        [HttpPost("enviar_para_usuario")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<bool>> EnviarParaUsuario(Guid id_usuario, [FromBody] MensagemSummary mensagem)
        {
            return await base.ResponseAsync(
                await _MensagemService.EnviarParaUsuario(id_usuario, mensagem), _MensagemService);
        }

        /// <summary>
        /// Envia uma mensagem para vários usuários (retorna a qtd de mensagens enviadas).
        /// </summary>
        [HttpPost("enviar_para_usuarios")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<int>> EnviarParaUsuarios([FromBody] MensagemMultiUsuarios mensagem_usuarios)
        {
            return await base.ResponseAsync(
                await _MensagemService.EnviarParaUsuarios(mensagem_usuarios.ids_usuarios, mensagem_usuarios.mensagem), _MensagemService);
        }

        /// <summary>
        /// Envia uma mensagem para um grupo de usuários (retorna a qtd de mensagens enviadas).
        /// </summary>
        [HttpPost("enviar_para_grupo_usuarios")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<int>> EnviarParaGrupoUsuarios(Guid id_grupo_usuario, [FromBody] MensagemSummary mensagem)
        {
            return await base.ResponseAsync(
                await _MensagemService.EnviarParaGrupoUsuarios(id_grupo_usuario, mensagem), _MensagemService);
        }

        /// <summary>
        /// Envia uma mensagem para um grupo de usuários (retorna a qtd de mensagens enviadas).
        /// </summary>
        [HttpPost("alterar_status_msg")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<bool>> AlterarStatusMensagem(Guid id_mensagem, Guid id_usuario, StatusMensagem status)
        {
            return await base.ResponseAsync(
                await _MensagemService.AlterarStatusMensagem(id_mensagem, id_usuario, status), _MensagemService);
        }
    }
}