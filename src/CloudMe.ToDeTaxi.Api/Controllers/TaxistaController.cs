using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class TaxistaController : BaseController
    {
        ITaxistaService _taxistaService;

        public TaxistaController(ITaxistaService taxistaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _taxistaService = taxistaService;
        }

        /// <summary>
        /// Gets all taxistas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaxistaSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _taxistaService.GetAllSummariesAsync(), _taxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Taxista.
        /// <param name="id">Taxista's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaxistaSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _taxistaService.GetSummaryAsync(id), _taxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Taxista.
        /// </summary>
        /// <param name="taxistaSummary">Taxista's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] TaxistaSummary taxistaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._taxistaService.CreateAsync(taxistaSummary) != null, _taxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing Taxista.
        /// </summary>
        /// <param name="taxistaSummary">Modified Taxista list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] TaxistaSummary taxistaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._taxistaService.UpdateAsync(taxistaSummary) != null, _taxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Taxista.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._taxistaService.DeleteAsync(id), _taxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}