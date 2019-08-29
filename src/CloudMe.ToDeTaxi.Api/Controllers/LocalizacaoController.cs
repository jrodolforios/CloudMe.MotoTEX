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
    public class LocalizacaoController : BaseController
    {
        ILocalizacaoService _localizacaoService;

        public LocalizacaoController(ILocalizacaoService localizacaoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _localizacaoService = localizacaoService;
        }

        /// <summary>
        /// Gets all localizacaos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocalizacaoSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _localizacaoService.GetAllSummariesAsync(), _localizacaoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Localizacao.
        /// <param name="id">Localizacao's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LocalizacaoSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _localizacaoService.GetSummaryAsync(id), _localizacaoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Localizacao.
        /// </summary>
        /// <param name="localizacaoSummary">Localizacao's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] LocalizacaoSummary localizacaoSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._localizacaoService.CreateAsync(localizacaoSummary) != null, _localizacaoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing Localizacao.
        /// </summary>
        /// <param name="localizacaoSummary">Modified Localizacao list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] LocalizacaoSummary localizacaoSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._localizacaoService.UpdateAsync(localizacaoSummary) != null, _localizacaoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Localizacao.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._localizacaoService.DeleteAsync(id), _localizacaoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}