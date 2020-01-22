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
    public class FaturamentoController : BaseController
    {
        IFaturamentoService _faturamentoService;

        public FaturamentoController(IFaturamentoService faturamentoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _faturamentoService = faturamentoService;
        }

        /// <summary>
        /// Gets all Faturamento.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<FaturamentoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<FaturamentoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _faturamentoService.GetAllSummariesAsync(), _faturamentoService);
        }

        /// <summary>
        /// Gets a Faturamento.
        /// <param name="id">Faturamento's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<FaturamentoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<FaturamentoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _faturamentoService.GetSummaryAsync(id), _faturamentoService);
        }

        /// <summary>
        /// Creates a new Faturamento.
        /// </summary>
        /// <param name="FaturamentoSummary">Faturamento's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] FaturamentoSummary FaturamentoSummary)
        {
            var entity = await this._faturamentoService.CreateAsync(FaturamentoSummary);
            if (_faturamentoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_faturamentoService);
            }
            return await base.ResponseAsync(entity.Id, _faturamentoService);
        }

        /// <summary>
        /// Modifies an existing Faturamento.
        /// </summary>
        /// <param name="FaturamentoSummary">Modified Faturamento list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] FaturamentoSummary FaturamentoSummary)
        {
            return await base.ResponseAsync(await this._faturamentoService.UpdateAsync(FaturamentoSummary) != null, _faturamentoService);
        }

        /// <summary>
        /// Removes an existing Faturamento.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            var corSummary = await _faturamentoService.GetSummaryAsync(id);
            if (corSummary.Id == Guid.Empty)
            {
                _faturamentoService.AddNotification(new Notification("Cores", "Faturamento não encontrada"));
            }

            return await base.ResponseAsync(await this._faturamentoService.DeleteAsync(id), _faturamentoService);
        }
    }
}