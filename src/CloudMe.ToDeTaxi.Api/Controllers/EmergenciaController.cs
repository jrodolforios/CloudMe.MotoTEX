using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class EmergenciaController : BaseController
    {
        IEmergenciaService _EmergenciaService;

        public EmergenciaController(IEmergenciaService EmergenciaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _EmergenciaService = EmergenciaService;
        }

        /// <summary>
        /// Gets all Emergencias.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<EmergenciaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<EmergenciaSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _EmergenciaService.GetAllSummariesAsync(), _EmergenciaService);
        }

        /// <summary>
        /// Gets a Emergencia.
        /// <param name="id">Emergencia's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<EmergenciaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<EmergenciaSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _EmergenciaService.GetSummaryAsync(id), _EmergenciaService);
        }

        /// <summary>
        /// Creates a new Emergencia.
        /// </summary>
        /// <param name="EmergenciaSummary">Emergencia's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] EmergenciaSummary EmergenciaSummary)
        {
            var entity = await this._EmergenciaService.CreateAsync(EmergenciaSummary);
            if (_EmergenciaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_EmergenciaService);
            }
            return await base.ResponseAsync(entity.Id, _EmergenciaService);
        }

        /// <summary>
        /// Modifies an existing Emergencia.
        /// </summary>
        /// <param name="EmergenciaSummary">Modified Emergencia list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] EmergenciaSummary EmergenciaSummary)
        {
            return await base.ResponseAsync(await this._EmergenciaService.UpdateAsync(EmergenciaSummary) != null, _EmergenciaService);
        }

        /// <summary>
        /// Removes an existing Emergencia.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._EmergenciaService.DeleteAsync(id), _EmergenciaService);
        }

        /// <summary>
        /// Criação simplificada de uma emergência.
        /// </summary>
        [HttpPost("panico")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Panico(Guid id_taxista, string latitude, string longitude)
        {
            return await base.ResponseAsync(await _EmergenciaService.Panico(id_taxista, latitude, longitude), _EmergenciaService);
        }

        /// <summary>
        /// Altera o status de uma emergência.
        /// </summary>
        [HttpPost("alterar_status")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> AlterarStatus(Guid id_emergencia, StatusEmergencia status)
        {
            return await base.ResponseAsync(await _EmergenciaService.AlterarStatus(id_emergencia, status), _EmergenciaService);
        }
    }
}