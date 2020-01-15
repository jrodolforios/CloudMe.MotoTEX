using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using Microsoft.AspNetCore.Cors;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using CloudMe.MotoTEX.Api.Models;

namespace CloudMe.MotoTEX.Api.Controllers
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
        /// Classifica Taxista
        /// </summary>
        [HttpGet("classifica_taxista/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> ClassificaTaxista(Guid id,int classificacao)
        {
            return await base.ResponseAsync(await _corridaService.ClassificaTaxista(id, classificacao), _corridaService);
        }

        /// <summary>
        /// Classifica passageiro
        /// </summary>
        [HttpGet("classificar_passageiro/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> ClassificaPassageiro(Guid id, int classificacao)
        {
            return await base.ResponseAsync(await _corridaService.ClassificaPassageiro(id, classificacao), _corridaService);
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

        /// <summary>
        /// Pausa uma corrida.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpPost("pausar_corrida/{id}")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<Response<int>> PausarCorrida(Guid id)
        {
            return await base.ResponseAsync(await this._corridaService.PausarCorrida(id), _corridaService);
        }

        /// <summary>
        /// Pausa uma corrida.
        /// </summary>
        /// <param name="data">DialList's ID</param>
        [HttpPost("recuperar_apartir_de_data/{data}")]
        [ProducesResponseType(typeof(Response<List<CorridaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<List<CorridaSummary>>> RecuperarAPartirDeData(DateTime data)
        {
            return await ResponseAsync(await _corridaService.RecuperarAPartirDeData(data), _corridaService);
        }

        /// <summary>
        /// Retoma uma corrida pausada.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpPost("retomar_corrida/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> RetomarCorrida(Guid id)
        {
            return await base.ResponseAsync(await this._corridaService.RetomarCorrida(id), _corridaService);
        }
    }
}