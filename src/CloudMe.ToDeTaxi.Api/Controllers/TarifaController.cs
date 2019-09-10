using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class TarifaController : BaseController
    {
        ITarifaService _tarifaService;

        public TarifaController(ITarifaService tarifaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _tarifaService = tarifaService;
        }

        /// <summary>
        /// Gets all tarifas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TarifaSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _tarifaService.GetAllSummariesAsync(), _tarifaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Tarifa.
        /// <param name="id">Tarifa's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TarifaSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _tarifaService.GetSummaryAsync(id), _tarifaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Tarifa.
        /// </summary>
        /// <param name="tarifaSummary">Tarifa's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] TarifaSummary tarifaSummary)
        {
            try
            {
                return await base.ResponseAsync((await this._tarifaService.CreateAsync(tarifaSummary)).Id, _tarifaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing Tarifa.
        /// </summary>
        /// <param name="tarifaSummary">Modified Tarifa list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] TarifaSummary tarifaSummary)
        {
            try
            {
                return await base.ResponseAsync((await this._tarifaService.UpdateAsync(tarifaSummary)).Id, _tarifaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Tarifa.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._tarifaService.DeleteAsync(id), _tarifaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}