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
    public class FaixaDescontoController : BaseController
    {
        IFaixaDescontoService _faixaDescontoService;

        public FaixaDescontoController(IFaixaDescontoService faixaDescontoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _faixaDescontoService = faixaDescontoService;
        }

        /// <summary>
        /// Gets all faixaDescontos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FaixaDescontoSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _faixaDescontoService.GetAllSummariesAsync(), _faixaDescontoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a FaixaDesconto.
        /// <param name="id">FaixaDesconto's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FaixaDescontoSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _faixaDescontoService.GetSummaryAsync(id), _faixaDescontoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new FaixaDesconto.
        /// </summary>
        /// <param name="faixaDescontoSummary">FaixaDesconto's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] FaixaDescontoSummary faixaDescontoSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._faixaDescontoService.CreateAsync(faixaDescontoSummary) != null, _faixaDescontoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing FaixaDesconto.
        /// </summary>
        /// <param name="faixaDescontoSummary">Modified FaixaDesconto list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] FaixaDescontoSummary faixaDescontoSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._faixaDescontoService.UpdateAsync(faixaDescontoSummary) != null, _faixaDescontoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing FaixaDesconto.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._faixaDescontoService.DeleteAsync(id), _faixaDescontoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}