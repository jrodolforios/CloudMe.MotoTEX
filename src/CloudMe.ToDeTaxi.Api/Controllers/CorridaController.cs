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
    public class CorridaController : BaseController
    {
        ICorridaService _corridaService;

        public CorridaController(ICorridaService corridaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _corridaService = corridaService;
        }

        /// <summary>
        /// Gets all corridas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CorridaSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _corridaService.GetAllSummariesAsync(), _corridaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Corrida.
        /// <param name="id">Corrida's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CorridaSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _corridaService.GetSummaryAsync(id), _corridaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Corrida.
        /// </summary>
        /// <param name="corridaSummary">Corrida's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] CorridaSummary corridaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._corridaService.CreateAsync(corridaSummary) != null, _corridaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing Corrida.
        /// </summary>
        /// <param name="corridaSummary">Modified Corrida list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] CorridaSummary corridaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._corridaService.UpdateAsync(corridaSummary) != null, _corridaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Corrida.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._corridaService.DeleteAsync(id), _corridaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}