using System;
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
    public class TarifaController : BaseController
    {
        ITarifaService _TarifaService;

        public TarifaController(ITarifaService TarifaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _TarifaService = TarifaService;
        }

        /// <summary>
        /// Gets all Tarifas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<TarifaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<TarifaSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _TarifaService.GetAllSummariesAsync(), _TarifaService);
        }

        /// <summary>
        /// Return de tax of the ride to current Time
        /// </summary>
        /// <returns>passenger</returns>
        [HttpGet("GetValorKmRodadoAtual")]
        [ProducesResponseType(typeof(Response<decimal>), (int)HttpStatusCode.OK)]
        public async Task<Response<decimal>> IsBandeira2(decimal kilometers)
        {
            return await base.ResponseAsync(_TarifaService.GetValorCorrida(DateTime.Now, kilometers), _TarifaService);
        }

        /// <summary>
        /// Gets a Tarifa.
        /// <param name="id">Tarifa's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<TarifaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<TarifaSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _TarifaService.GetSummaryAsync(id), _TarifaService);
        }

        /// <summary>
        /// Creates a new Tarifa.
        /// </summary>
        /// <param name="TarifaSummary">Tarifa's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] TarifaSummary TarifaSummary)
        {
            var entity = await this._TarifaService.CreateAsync(TarifaSummary);
            if (_TarifaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_TarifaService);
            }
            return await base.ResponseAsync(entity.Id, _TarifaService);
        }

        /// <summary>
        /// Modifies an existing Tarifa.
        /// </summary>
        /// <param name="TarifaSummary">Modified Tarifa list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] TarifaSummary TarifaSummary)
        {
            return await base.ResponseAsync(await this._TarifaService.UpdateAsync(TarifaSummary) != null, _TarifaService);
        }

        /// <summary>
        /// Removes an existing Tarifa.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._TarifaService.DeleteAsync(id), _TarifaService);
        }
    }
}