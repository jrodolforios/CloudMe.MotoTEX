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
using CloudMe.ToDeTaxi.Domain.Model;

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
        /// Obtém as mensagens enviadas por um usuário.
        /// </summary>
        [HttpPost("obter_enviadas")]
        [ProducesResponseType(typeof(Response<IEnumerable<DetalhesMensagem>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<DetalhesMensagem>>> ObterMensagensEnviadas(Guid id_usuario, DateTime? inicio, DateTime? fim, Pagination pagination)
        {
            int count = 0;
            var response = await base.ResponseAsync(
                await _MensagemService.ObterMensagensEnviadas(id_usuario, inicio, fim, pagination, out count), _MensagemService);

            if (response.success)
            {
                response.count = count;
            }
            return response;
        }

        [HttpPost("obter_enviadas_marcar_idas")]
        [ProducesResponseType(typeof(Response<List<DetalhesMensagem>>), (int)HttpStatusCode.OK)]
        public async Task<Response<List<DetalhesMensagem>>> ObterMensagensEnviadasEMarcarLidas(Guid id_usuario)
        {
            int count = 0;
            var response = await base.ResponseAsync(
                await _MensagemService.ObterMensagensEnviadasEMarcarLidas(id_usuario), _MensagemService);

            if (response.success)
            {
                response.count = count;
            }
            return response;
        }

        /// <summary>
        /// Obtém as mensagens recebidas por um usuário.
        /// </summary>
        [HttpPost("obter_recebidas")]
        [ProducesResponseType(typeof(Response<IEnumerable<DetalhesMensagem>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<DetalhesMensagem>>> ObterMensagensRecebidas(Guid id_usuario, DateTime? inicio, DateTime? fim, Pagination pagination)
        {
            int count = 0;
            var response = await base.ResponseAsync(
                await _MensagemService.ObterMensagensRecebidas(id_usuario, inicio, fim, pagination, out count), _MensagemService);

            if (response.success)
            {
                response.count = count;
            }
            return response;
        }

        /// <summary>
        /// Envia uma mensagem para um usuário.
        /// </summary>
        [HttpPost("enviar")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<int>> Enviar([FromBody] ParametrosEnvio parametrosEnvio)
        {
            return await base.ResponseAsync(
                await _MensagemService.Enviar(parametrosEnvio.mensagem, parametrosEnvio.destinatarios), _MensagemService);
        }

        /// <summary>
        /// Encaminha uma mensagem para um usuário.
        /// </summary>
        /*
         * TODO: AINDA EM ANÁLISE
         * 
         * [HttpPost("encaminhar")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<int>> Encaminhar(Guid id_mensagem, DestinatariosMensagem destinatarios)
        {
            return await base.ResponseAsync(
                await _MensagemService.Encaminhar(id_mensagem, destinatarios), _MensagemService);
        }*/

        /// <summary>
        /// Obtém o recibo de envio de uma mensagem a um destinatário.
        /// </summary>
        [HttpGet("recibo")]
        [ProducesResponseType(typeof(Response<MensagemDestinatarioSummary>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<IEnumerable<MensagemDestinatarioSummary>>> ObterRecibosMensagem(Guid id_mensagem, Guid id_usuario)
        {
            return await base.ResponseAsync(
                await _MensagemService.ObterRecibosMensagem(id_mensagem, id_usuario), _MensagemService);
        }

        /// <summary>
        /// Envia uma mensagem para um grupo de usuários (retorna a qtd de mensagens enviadas).
        /// </summary>
        [HttpPost("alterar_status")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<bool>> AlterarStatusMensagem(Guid id_mensagem, Guid id_usuario, StatusMensagem status)
        {
            return await base.ResponseAsync(
                await _MensagemService.AlterarStatusMensagem(id_mensagem, id_usuario, status), _MensagemService);
        }
    }
}