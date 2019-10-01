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
    public class FaixaDescontoController : BaseController
    {
        IFaixaDescontoService _FaixaDescontoService;

        public FaixaDescontoController(IFaixaDescontoService FaixaDescontoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _FaixaDescontoService = FaixaDescontoService;
        }

        /// <summary>
        /// Gets all FaixaDescontos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<FaixaDescontoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<FaixaDescontoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _FaixaDescontoService.GetAllSummariesAsync(), _FaixaDescontoService);
        }

        /// <summary>
        /// Gets a FaixaDesconto.
        /// <param name="id">FaixaDesconto's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<FaixaDescontoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<FaixaDescontoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _FaixaDescontoService.GetSummaryAsync(id), _FaixaDescontoService);
        }

        /// <summary>
        /// Creates a new FaixaDesconto.
        /// </summary>
        /// <param name="FaixaDescontoSummary">FaixaDesconto's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] FaixaDescontoSummary FaixaDescontoSummary)
        {
            var entity = await this._FaixaDescontoService.CreateAsync(FaixaDescontoSummary);
            if (_FaixaDescontoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_FaixaDescontoService);
            }
            return await base.ResponseAsync(entity.Id, _FaixaDescontoService);
        }

        /// <summary>
        /// Modifies an existing FaixaDesconto.
        /// </summary>
        /// <param name="FaixaDescontoSummary">Modified FaixaDesconto list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] FaixaDescontoSummary FaixaDescontoSummary)
        {
            return await base.ResponseAsync(await this._FaixaDescontoService.UpdateAsync(FaixaDescontoSummary) != null, _FaixaDescontoService);
        }

        /// <summary>
        /// Removes an existing FaixaDesconto.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._FaixaDescontoService.DeleteAsync(id), _FaixaDescontoService);
        }
    }
}