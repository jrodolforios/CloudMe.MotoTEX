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
    public class FormaPagamentoController : BaseController
    {
        IFormaPagamentoService _FormaPagamentoService;

        public FormaPagamentoController(IFormaPagamentoService FormaPagamentoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _FormaPagamentoService = FormaPagamentoService;
        }

        /// <summary>
        /// Gets all FormaPagamentos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<FormaPagamentoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<FormaPagamentoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _FormaPagamentoService.GetAllSummariesAsync(), _FormaPagamentoService);
        }

        /// <summary>
        /// Gets a FormaPagamento.
        /// <param name="id">FormaPagamento's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<FormaPagamentoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<FormaPagamentoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _FormaPagamentoService.GetSummaryAsync(id), _FormaPagamentoService);
        }

        /// <summary>
        /// Creates a new FormaPagamento.
        /// </summary>
        /// <param name="FormaPagamentoSummary">FormaPagamento's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] FormaPagamentoSummary FormaPagamentoSummary)
        {
            var entity = await this._FormaPagamentoService.CreateAsync(FormaPagamentoSummary);
            if (_FormaPagamentoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_FormaPagamentoService);
            }
            return await base.ResponseAsync(entity.Id, _FormaPagamentoService);
        }

        /// <summary>
        /// Modifies an existing FormaPagamento.
        /// </summary>
        /// <param name="FormaPagamentoSummary">Modified FormaPagamento list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] FormaPagamentoSummary FormaPagamentoSummary)
        {
            return await base.ResponseAsync(await this._FormaPagamentoService.UpdateAsync(FormaPagamentoSummary) != null, _FormaPagamentoService);
        }

        /// <summary>
        /// Removes an existing FormaPagamento.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._FormaPagamentoService.DeleteAsync(id), _FormaPagamentoService);
        }
    }
}