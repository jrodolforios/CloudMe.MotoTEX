using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Call;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class CallController : BaseController
    {
        ICallService _callService;

        public CallController(ICallService callService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _callService = callService;
        }

        /// <summary>
        /// Gets all calls.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CallSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _callService.GetAllSummariesAsync(), _callService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Call.
        /// <param name="id">Call's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CallSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _callService.GetSummaryAsync(id), _callService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Call.
        /// </summary>
        /// <param name="callSummary">Call's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] CallSummary callSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._callService.CreateAsync(callSummary) != null, _callService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing Call.
        /// </summary>
        /// <param name="callSummary">Modified Call list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] CallSummary callSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._callService.UpdateAsync(callSummary) != null, _callService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Call.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._callService.DeleteAsync(id), _callService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}