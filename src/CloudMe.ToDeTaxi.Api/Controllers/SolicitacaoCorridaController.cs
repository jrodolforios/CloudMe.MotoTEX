﻿using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class SolicitacaoCorridaController : BaseController
    {
        ISolicitacaoCorridaService _SolicitacaoCorridaService;

        public SolicitacaoCorridaController(ISolicitacaoCorridaService SolicitacaoCorridaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _SolicitacaoCorridaService = SolicitacaoCorridaService;
        }

        /// <summary>
        /// Gets all SolicitacaoCorridas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<SolicitacaoCorridaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<SolicitacaoCorridaSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _SolicitacaoCorridaService.GetAllSummariesAsync(), _SolicitacaoCorridaService);
        }

        /// <summary>
        /// Gets a SolicitacaoCorrida.
        /// <param name="id">SolicitacaoCorrida's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<SolicitacaoCorridaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<SolicitacaoCorridaSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _SolicitacaoCorridaService.GetSummaryAsync(id), _SolicitacaoCorridaService);
        }

        /// <summary>
        /// Creates a new SolicitacaoCorrida.
        /// </summary>
        /// <param name="SolicitacaoCorridaSummary">SolicitacaoCorrida's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] SolicitacaoCorridaSummary SolicitacaoCorridaSummary)
        {
            var entity = await this._SolicitacaoCorridaService.CreateAsync(SolicitacaoCorridaSummary);
            if (_SolicitacaoCorridaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_SolicitacaoCorridaService);
            }
            return await base.ResponseAsync(entity.Id, _SolicitacaoCorridaService);
        }

        /// <summary>
        /// Modifies an existing SolicitacaoCorrida.
        /// </summary>
        /// <param name="SolicitacaoCorridaSummary">Modified SolicitacaoCorrida list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] SolicitacaoCorridaSummary SolicitacaoCorridaSummary)
        {
            return await base.ResponseAsync(await this._SolicitacaoCorridaService.UpdateAsync(SolicitacaoCorridaSummary) != null, _SolicitacaoCorridaService);
        }

        /// <summary>
        /// Removes an existing SolicitacaoCorrida.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._SolicitacaoCorridaService.DeleteAsync(id), _SolicitacaoCorridaService);
        }

        /// <summary>
        /// Informa a ação do taxista a uma solicitação de corrida.
        /// </summary>
        /// <param name="id_solicitacao">Id da solicitação</param>
        /// <param name="id_taxista">Id do taxista</param>
        /// <param name="acao">Ação tomada pelo taxista na solicitação</param>
        [HttpPost("acao_taxista/{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> AcaoTaxistaSolicitacao(Guid id_solicitacao, Guid id_taxista, AcaoTaxistaSolicitacaoCorrida acao)
        {
            return await base.ResponseAsync(await this._SolicitacaoCorridaService.RegistrarAcaoTaxista(id_solicitacao, id_taxista, acao), _SolicitacaoCorridaService);
        }

        /// <summary>
        /// Recupera Solicitações ainda não atendidas
        /// </summary>
        [HttpPost("recuperar_solicitacoes_em_espera")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<IList<SolicitacaoCorridaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IList<SolicitacaoCorridaSummary>>>   RecuperarSolicitacoesEmEspera(string IdTaxista)
        {
            return await base.ResponseAsync(await this._SolicitacaoCorridaService.RecuperarSolicitacoesEmEspera(IdTaxista), _SolicitacaoCorridaService);
        }

    }
}