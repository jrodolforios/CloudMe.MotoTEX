using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class RotaController : BaseController
    {
        IRotaService _rotaService;

        public RotaController(IRotaService rotaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _rotaService = rotaService;
        }

        /// <summary>
        /// Gets all rotas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RotaSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _rotaService.GetAllSummariesAsync(), _rotaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Rota.
        /// <param name="id">Rota's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RotaSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _rotaService.GetSummaryAsync(id), _rotaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Rota.
        /// </summary>
        /// <param name="rotaSummary">Rota's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] RotaSummary rotaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._rotaService.CreateAsync(rotaSummary) != null, _rotaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing Rota.
        /// </summary>
        /// <param name="rotaSummary">Modified Rota list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] RotaSummary rotaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._rotaService.UpdateAsync(rotaSummary) != null, _rotaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Rota.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._rotaService.DeleteAsync(id), _rotaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}