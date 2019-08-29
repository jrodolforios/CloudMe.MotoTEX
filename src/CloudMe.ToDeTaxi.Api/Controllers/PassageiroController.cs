using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class PassageiroController : BaseController
    {
        IPassageiroService _passageiroService;

        public PassageiroController(IPassageiroService passageiroService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _passageiroService = passageiroService;
        }

        /// <summary>
        /// Gets all passageiros.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PassageiroSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _passageiroService.GetAllSummariesAsync(), _passageiroService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Passageiro.
        /// <param name="id">Passageiro's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PassageiroSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _passageiroService.GetSummaryAsync(id), _passageiroService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Passageiro.
        /// </summary>
        /// <param name="passageiroSummary">Passageiro's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] PassageiroSummary passageiroSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._passageiroService.CreateAsync(passageiroSummary) != null, _passageiroService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing Passageiro.
        /// </summary>
        /// <param name="passageiroSummary">Modified Passageiro list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] PassageiroSummary passageiroSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._passageiroService.UpdateAsync(passageiroSummary) != null, _passageiroService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Passageiro.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._passageiroService.DeleteAsync(id), _passageiroService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}