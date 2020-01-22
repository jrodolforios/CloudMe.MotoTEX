using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Veiculo;
using Microsoft.AspNetCore.Cors;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using CloudMe.MotoTEX.Api.Models;
using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Model.Faturamento;

namespace CloudMe.MotoTEX.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class FaturamentoTaxistaController : BaseController
    {
        IFaturamentoTaxistaService _FaturamentoTaxistaService;

        public FaturamentoTaxistaController(IFaturamentoTaxistaService FaturamentoTaxistaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _FaturamentoTaxistaService = FaturamentoTaxistaService;
        }

        /// <summary>
        /// Gets all FaturamentoTaxista.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<FaturamentoTaxistaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<FaturamentoTaxistaSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _FaturamentoTaxistaService.GetAllSummariesAsync(), _FaturamentoTaxistaService);
        }

        /// <summary>
        /// Gets a FaturamentoTaxista.
        /// <param name="id">FaturamentoTaxista's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<FaturamentoTaxistaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<FaturamentoTaxistaSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _FaturamentoTaxistaService.GetSummaryAsync(id), _FaturamentoTaxistaService);
        }

        /// <summary>
        /// Creates a new FaturamentoTaxista.
        /// </summary>
        /// <param name="FaturamentoTaxistaSummary">FaturamentoTaxista's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] FaturamentoTaxistaSummary FaturamentoTaxistaSummary)
        {
            var entity = await this._FaturamentoTaxistaService.CreateAsync(FaturamentoTaxistaSummary);
            if (_FaturamentoTaxistaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_FaturamentoTaxistaService);
            }
            return await base.ResponseAsync(entity.Id, _FaturamentoTaxistaService);
        }

        /// <summary>
        /// Modifies an existing FaturamentoTaxista.
        /// </summary>
        /// <param name="FaturamentoTaxistaSummary">Modified FaturamentoTaxista list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] FaturamentoTaxistaSummary FaturamentoTaxistaSummary)
        {
            return await base.ResponseAsync(await this._FaturamentoTaxistaService.UpdateAsync(FaturamentoTaxistaSummary) != null, _FaturamentoTaxistaService);
        }

        /// <summary>
        /// Removes an existing FaturamentoTaxista.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            var corSummary = await _FaturamentoTaxistaService.GetSummaryAsync(id);
            if (corSummary.Id == Guid.Empty)
            {
                _FaturamentoTaxistaService.AddNotification(new Notification("Cores", "FaturamentoTaxista não encontrada"));
            }

            return await base.ResponseAsync(await this._FaturamentoTaxistaService.DeleteAsync(id), _FaturamentoTaxistaService);
        }
    }
}