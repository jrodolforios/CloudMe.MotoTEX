using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Veiculo;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class VeiculoController : BaseController
    {
        IVeiculoService _veiculoService;

        public VeiculoController(IVeiculoService veiculoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _veiculoService = veiculoService;
        }

        /// <summary>
        /// Gets all veiculos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VeiculoSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _veiculoService.GetAllSummariesAsync(), _veiculoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Veiculo.
        /// <param name="id">Veiculo's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(VeiculoSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _veiculoService.GetSummaryAsync(id), _veiculoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Veiculo.
        /// </summary>
        /// <param name="veiculoSummary">Veiculo's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] VeiculoSummary veiculoSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._veiculoService.CreateAsync(veiculoSummary) != null, _veiculoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing Veiculo.
        /// </summary>
        /// <param name="veiculoSummary">Modified Veiculo list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] VeiculoSummary veiculoSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._veiculoService.UpdateAsync(veiculoSummary) != null, _veiculoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Veiculo.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._veiculoService.DeleteAsync(id), _veiculoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}