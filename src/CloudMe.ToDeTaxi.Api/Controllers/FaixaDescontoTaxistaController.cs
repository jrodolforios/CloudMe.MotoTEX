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
    public class FaixaDescontoTaxistaController : BaseController
    {
        IFaixaDescontoTaxistaService _faixaDescontoTaxistaService;

        public FaixaDescontoTaxistaController(IFaixaDescontoTaxistaService faixaDescontoTaxistaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _faixaDescontoTaxistaService = faixaDescontoTaxistaService;
        }

        /// <summary>
        /// Gets all faixaDescontoTaxistas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FaixaDescontoTaxistaSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _faixaDescontoTaxistaService.GetAllSummariesAsync(), _faixaDescontoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a FaixaDescontoTaxista.
        /// <param name="id">FaixaDescontoTaxista's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FaixaDescontoTaxistaSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _faixaDescontoTaxistaService.GetSummaryAsync(id), _faixaDescontoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new FaixaDescontoTaxista.
        /// </summary>
        /// <param name="faixaDescontoTaxistaSummary">FaixaDescontoTaxista's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] FaixaDescontoTaxistaSummary faixaDescontoTaxistaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._faixaDescontoTaxistaService.CreateAsync(faixaDescontoTaxistaSummary) != null, _faixaDescontoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing FaixaDescontoTaxista.
        /// </summary>
        /// <param name="faixaDescontoTaxistaSummary">Modified FaixaDescontoTaxista list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] FaixaDescontoTaxistaSummary faixaDescontoTaxistaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._faixaDescontoTaxistaService.UpdateAsync(faixaDescontoTaxistaSummary) != null, _faixaDescontoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing FaixaDescontoTaxista.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._faixaDescontoTaxistaService.DeleteAsync(id), _faixaDescontoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}