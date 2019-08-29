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
    public class FormaPagamentoTaxistaController : BaseController
    {
        IFormaPagamentoTaxistaService _formaPagamentoTaxistaService;

        public FormaPagamentoTaxistaController(IFormaPagamentoTaxistaService formaPagamentoTaxistaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _formaPagamentoTaxistaService = formaPagamentoTaxistaService;
        }

        /// <summary>
        /// Gets all formaPagamentoTaxistas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FormaPagamentoTaxistaSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _formaPagamentoTaxistaService.GetAllSummariesAsync(), _formaPagamentoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a FormaPagamentoTaxista.
        /// <param name="id">FormaPagamentoTaxista's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormaPagamentoTaxistaSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _formaPagamentoTaxistaService.GetSummaryAsync(id), _formaPagamentoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new FormaPagamentoTaxista.
        /// </summary>
        /// <param name="formaPagamentoTaxistaSummary">FormaPagamentoTaxista's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] FormaPagamentoTaxistaSummary formaPagamentoTaxistaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._formaPagamentoTaxistaService.CreateAsync(formaPagamentoTaxistaSummary) != null, _formaPagamentoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing FormaPagamentoTaxista.
        /// </summary>
        /// <param name="formaPagamentoTaxistaSummary">Modified FormaPagamentoTaxista list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] FormaPagamentoTaxistaSummary formaPagamentoTaxistaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._formaPagamentoTaxistaService.UpdateAsync(formaPagamentoTaxistaSummary) != null, _formaPagamentoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing FormaPagamentoTaxista.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._formaPagamentoTaxistaService.DeleteAsync(id), _formaPagamentoTaxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}