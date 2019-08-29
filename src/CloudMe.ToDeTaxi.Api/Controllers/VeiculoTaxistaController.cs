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
    public class VeiculoTaxistaController : BaseController
    {
        IVeiculoTaxistaService _veiculoTaxistaService;

        public VeiculoTaxistaController(IVeiculoTaxistaService veiculoTaxistaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _veiculoTaxistaService = veiculoTaxistaService;
        }

        /// <summary>
        /// Gets all veiculoTaxistas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VeiculoTaxistaSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _veiculoTaxistaService.GetAllSummariesAsync(), _veiculoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a VeiculoTaxista.
        /// <param name="id">VeiculoTaxista's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(VeiculoTaxistaSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _veiculoTaxistaService.GetSummaryAsync(id), _veiculoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new VeiculoTaxista.
        /// </summary>
        /// <param name="veiculoTaxistaSummary">VeiculoTaxista's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] VeiculoTaxistaSummary veiculoTaxistaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._veiculoTaxistaService.CreateAsync(veiculoTaxistaSummary) != null, _veiculoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing VeiculoTaxista.
        /// </summary>
        /// <param name="veiculoTaxistaSummary">Modified VeiculoTaxista list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] VeiculoTaxistaSummary veiculoTaxistaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._veiculoTaxistaService.UpdateAsync(veiculoTaxistaSummary) != null, _veiculoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing VeiculoTaxista.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._veiculoTaxistaService.DeleteAsync(id), _veiculoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}