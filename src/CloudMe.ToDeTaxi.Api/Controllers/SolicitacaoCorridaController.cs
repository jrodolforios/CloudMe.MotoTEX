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
    public class SolicitacaoCorridaController : BaseController
    {
        ISolicitacaoCorridaService _solicitacaoCorridaService;

        public SolicitacaoCorridaController(ISolicitacaoCorridaService solicitacaoCorridaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _solicitacaoCorridaService = solicitacaoCorridaService;
        }

        /// <summary>
        /// Gets all solicitacaoCorridas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SolicitacaoCorridaSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _solicitacaoCorridaService.GetAllSummariesAsync(), _solicitacaoCorridaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a SolicitacaoCorrida.
        /// <param name="id">SolicitacaoCorrida's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SolicitacaoCorridaSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _solicitacaoCorridaService.GetSummaryAsync(id), _solicitacaoCorridaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new SolicitacaoCorrida.
        /// </summary>
        /// <param name="solicitacaoCorridaSummary">SolicitacaoCorrida's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] SolicitacaoCorridaSummary solicitacaoCorridaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._solicitacaoCorridaService.CreateAsync(solicitacaoCorridaSummary) != null, _solicitacaoCorridaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing SolicitacaoCorrida.
        /// </summary>
        /// <param name="solicitacaoCorridaSummary">Modified SolicitacaoCorrida list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] SolicitacaoCorridaSummary solicitacaoCorridaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._solicitacaoCorridaService.UpdateAsync(solicitacaoCorridaSummary) != null, _solicitacaoCorridaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing SolicitacaoCorrida.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._solicitacaoCorridaService.DeleteAsync(id), _solicitacaoCorridaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}