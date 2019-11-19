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

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class CorridaController : BaseController
    {
        ICorridaService _corridaService;

        public CorridaController(ICorridaService corridaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _corridaService = corridaService;
        }

        /// <summary>
        /// Gets all corridas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CorridaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<CorridaSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _corridaService.GetAllSummariesAsync(), _corridaService);
        }


        /// <summary>
        /// Gets all corridas by passageiro.
        /// </summary>
        [HttpGet("consulta_id_passageiro/{id}")]
        [ProducesResponseType(typeof(Response<IEnumerable<CorridaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<CorridaSummary>>> GetAllByPassanger(Guid id)
        {
            return await base.ResponseAsync(await _corridaService.GetAllSummariesByPassangerAsync(id), _corridaService);
        }

        /// <summary>
        /// Gets all corridas by passageiro.
        /// </summary>
        [HttpGet("consulta_id_taxista/{id}")]
        [ProducesResponseType(typeof(Response<IEnumerable<CorridaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<CorridaSummary>>> GetAllByTaxist(Guid id)
        {
            return await base.ResponseAsync(await _corridaService.GetAllSummariesByTaxistAsync(id), _corridaService);
        }

        /// <summary>
        /// Gets all corridas by passageiro.
        /// </summary>
        [HttpGet("consulta_id_solicitacao_corrida/{id}")]
        [ProducesResponseType(typeof(Response<CorridaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<CorridaSummary>> GetByIdSolicitacaoCorrida(Guid id)
        {
            return await base.ResponseAsync(await _corridaService.GetBySolicitacaoCorrida(id), _corridaService);
        }

        /// <summary>
        /// Gets a Corrida.
        /// <param name="id">Corrida's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<CorridaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<CorridaSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _corridaService.GetSummaryAsync(id), _corridaService);
        }

        /// <summary>
        /// Creates a new Corrida.
        /// </summary>
        /// <param name="corridaSummary">Corrida's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] CorridaSummary corridaSummary)
        {
            var entity = await this._corridaService.CreateAsync(corridaSummary);
            if (_corridaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_corridaService);
            }
            return await base.ResponseAsync(entity.Id, _corridaService);
        }

        /// <summary>
        /// Modifies an existing Corrida.
        /// </summary>
        /// <param name="corridaSummary">Modified Corrida list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] CorridaSummary corridaSummary)
        {
            return await base.ResponseAsync(await this._corridaService.UpdateAsync(corridaSummary) != null, _corridaService);
        }

        /// <summary>
        /// Removes an existing Corrida.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._corridaService.DeleteAsync(id), _corridaService);
        }
    }
}