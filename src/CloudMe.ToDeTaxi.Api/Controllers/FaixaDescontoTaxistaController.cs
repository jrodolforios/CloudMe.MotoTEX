using System;
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
    public class FaixaDescontoTaxistaController : BaseController
    {
        IFaixaDescontoTaxistaService _FaixaDescontoTaxistaService;

        public FaixaDescontoTaxistaController(IFaixaDescontoTaxistaService FaixaDescontoTaxistaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _FaixaDescontoTaxistaService = FaixaDescontoTaxistaService;
        }

        /// <summary>
        /// Gets all FaixaDescontoTaxistas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<FaixaDescontoTaxistaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<FaixaDescontoTaxistaSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _FaixaDescontoTaxistaService.GetAllSummariesAsync(), _FaixaDescontoTaxistaService);
        }

        /// <summary>
        /// Gets a FaixaDescontoTaxista.
        /// <param name="id">FaixaDescontoTaxista's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<FaixaDescontoTaxistaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<FaixaDescontoTaxistaSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _FaixaDescontoTaxistaService.GetSummaryAsync(id), _FaixaDescontoTaxistaService);
        }

        /// <summary>
        /// Creates a new FaixaDescontoTaxista.
        /// </summary>
        /// <param name="FaixaDescontoTaxistaSummary">FaixaDescontoTaxista's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] FaixaDescontoTaxistaSummary FaixaDescontoTaxistaSummary)
        {
            var entity = await this._FaixaDescontoTaxistaService.CreateAsync(FaixaDescontoTaxistaSummary);
            if (_FaixaDescontoTaxistaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_FaixaDescontoTaxistaService);
            }
            return await base.ResponseAsync(entity.Id, _FaixaDescontoTaxistaService);
        }

        /// <summary>
        /// Modifies an existing FaixaDescontoTaxista.
        /// </summary>
        /// <param name="FaixaDescontoTaxistaSummary">Modified FaixaDescontoTaxista list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] FaixaDescontoTaxistaSummary FaixaDescontoTaxistaSummary)
        {
            return await base.ResponseAsync(await this._FaixaDescontoTaxistaService.UpdateAsync(FaixaDescontoTaxistaSummary) != null, _FaixaDescontoTaxistaService);
        }

        /// <summary>
        /// Removes an existing FaixaDescontoTaxista.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._FaixaDescontoTaxistaService.DeleteAsync(id), _FaixaDescontoTaxistaService);
        }
    }
}