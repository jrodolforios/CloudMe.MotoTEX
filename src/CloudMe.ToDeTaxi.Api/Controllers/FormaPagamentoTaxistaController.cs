﻿using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class FormaPagamentoTaxistaController : BaseController
    {
        IFormaPagamentoTaxistaService _FormaPagamentoTaxistaService;

        public FormaPagamentoTaxistaController(IFormaPagamentoTaxistaService FormaPagamentoTaxistaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _FormaPagamentoTaxistaService = FormaPagamentoTaxistaService;
        }

        /// <summary>
        /// Gets all FormaPagamentoTaxistas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<FormaPagamentoTaxistaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<FormaPagamentoTaxistaSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _FormaPagamentoTaxistaService.GetAllSummariesAsync(), _FormaPagamentoTaxistaService);
        }

        /// <summary>
        /// Gets a FormaPagamentoTaxista.
        /// <param name="id">FormaPagamentoTaxista's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<FormaPagamentoTaxistaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<FormaPagamentoTaxistaSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _FormaPagamentoTaxistaService.GetSummaryAsync(id), _FormaPagamentoTaxistaService);
        }

        /// <summary>
        /// Creates a new FormaPagamentoTaxista.
        /// </summary>
        /// <param name="FormaPagamentoTaxistaSummary">FormaPagamentoTaxista's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] FormaPagamentoTaxistaSummary FormaPagamentoTaxistaSummary)
        {
            var entity = await this._FormaPagamentoTaxistaService.CreateAsync(FormaPagamentoTaxistaSummary);
            if (_FormaPagamentoTaxistaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_FormaPagamentoTaxistaService);
            }
            return await base.ResponseAsync(entity.Id, _FormaPagamentoTaxistaService);
        }

        /// <summary>
        /// Modifies an existing FormaPagamentoTaxista.
        /// </summary>
        /// <param name="FormaPagamentoTaxistaSummary">Modified FormaPagamentoTaxista list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] FormaPagamentoTaxistaSummary FormaPagamentoTaxistaSummary)
        {
            return await base.ResponseAsync(await this._FormaPagamentoTaxistaService.UpdateAsync(FormaPagamentoTaxistaSummary) != null, _FormaPagamentoTaxistaService);
        }

        /// <summary>
        /// Removes an existing FormaPagamentoTaxista.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._FormaPagamentoTaxistaService.DeleteAsync(id), _FormaPagamentoTaxistaService);
        }
    }
}