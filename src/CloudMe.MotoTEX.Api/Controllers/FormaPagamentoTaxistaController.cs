using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using Microsoft.AspNetCore.Cors;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using CloudMe.MotoTEX.Api.Models;

namespace CloudMe.MotoTEX.Api.Controllers
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
        /// Get by IdTaxista
        /// </summary>
        /// <param name="id">Id from taxist</param>
        /// <returns>passenger</returns>
        [HttpGet("consulta_id_taxista/{id}")]
        [ProducesResponseType(typeof(Response<List<FormaPagamentoTaxistaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<List<FormaPagamentoTaxistaSummary>>> GetByUserId(Guid id)
        {
            return await base.ResponseAsync(await _FormaPagamentoTaxistaService.GetByTaxistId(id), _FormaPagamentoTaxistaService);
        }

        /// <summary>
        /// Get by IdTaxista
        /// </summary>
        /// <param name="id">Id from taxist</param>
        /// <returns>passenger</returns>
        [HttpDelete("Deletar_por_taxista/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> DeletePorTaxista(Guid id)
        {
            return await base.ResponseAsync(await _FormaPagamentoTaxistaService.DeleteByTaxistId(id), _FormaPagamentoTaxistaService);
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