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
    public class EnderecoController : BaseController
    {
        IEnderecoService _EnderecoService;

        public EnderecoController(IEnderecoService EnderecoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _EnderecoService = EnderecoService;
        }

        /// <summary>
        /// Gets all Enderecos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EnderecoSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _EnderecoService.GetAllSummariesAsync(), _EnderecoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Endereco.
        /// <param name="id">Endereco's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EnderecoSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _EnderecoService.GetSummaryAsync(id), _EnderecoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Endereco.
        /// </summary>
        /// <param name="EnderecoSummary">Endereco's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] EnderecoSummary EnderecoSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._EnderecoService.CreateAsync(EnderecoSummary) != null, _EnderecoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing Endereco.
        /// </summary>
        /// <param name="EnderecoSummary">Modified Endereco list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] EnderecoSummary EnderecoSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._EnderecoService.UpdateAsync(EnderecoSummary) != null, _EnderecoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Endereco.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._EnderecoService.DeleteAsync(id), _EnderecoService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}