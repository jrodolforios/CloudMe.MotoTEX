using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;

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
        [ProducesResponseType(typeof(Response<IEnumerable<EnderecoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<EnderecoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _EnderecoService.GetAllSummariesAsync(), _EnderecoService);
        }

        /// <summary>
        /// Gets a Endereco.
        /// <param name="id">Endereco's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<EnderecoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<EnderecoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _EnderecoService.GetSummaryAsync(id), _EnderecoService);
        }

        /// <summary>
        /// Creates a new Endereco.
        /// </summary>
        /// <param name="EnderecoSummary">Endereco's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] EnderecoSummary EnderecoSummary)
        {
            var entity = await this._EnderecoService.CreateAsync(EnderecoSummary);
            if (_EnderecoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_EnderecoService);
            }
            return await base.ResponseAsync(entity.Id, _EnderecoService);
        }

        /// <summary>
        /// Modifies an existing Endereco.
        /// </summary>
        /// <param name="EnderecoSummary">Modified Endereco list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] EnderecoSummary EnderecoSummary)
        {
            return await base.ResponseAsync(await this._EnderecoService.UpdateAsync(EnderecoSummary) != null, _EnderecoService);
        }

        /// <summary>
        /// Removes an existing Endereco.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._EnderecoService.DeleteAsync(id), _EnderecoService);
        }
    }
}