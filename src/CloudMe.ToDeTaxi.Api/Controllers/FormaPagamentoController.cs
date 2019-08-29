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
    public class FormaPagamentoController : BaseController
    {
        IFormaPagamentoService _formaPagamentoService;

        public FormaPagamentoController(IFormaPagamentoService formaPagamentoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _formaPagamentoService = formaPagamentoService;
        }

        /// <summary>
        /// Gets all formaPagamentos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FormaPagamentoSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _formaPagamentoService.GetAllSummariesAsync(), _formaPagamentoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a FormaPagamento.
        /// <param name="id">FormaPagamento's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormaPagamentoSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _formaPagamentoService.GetSummaryAsync(id), _formaPagamentoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new FormaPagamento.
        /// </summary>
        /// <param name="formaPagamentoSummary">FormaPagamento's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] FormaPagamentoSummary formaPagamentoSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._formaPagamentoService.CreateAsync(formaPagamentoSummary) != null, _formaPagamentoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing FormaPagamento.
        /// </summary>
        /// <param name="formaPagamentoSummary">Modified FormaPagamento list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] FormaPagamentoSummary formaPagamentoSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._formaPagamentoService.UpdateAsync(formaPagamentoSummary) != null, _formaPagamentoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing FormaPagamento.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._formaPagamentoService.DeleteAsync(id), _formaPagamentoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}