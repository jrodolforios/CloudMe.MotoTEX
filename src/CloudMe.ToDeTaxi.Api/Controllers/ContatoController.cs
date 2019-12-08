using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;
using CloudMe.ToDeTaxi.Domain.Model;
using Microsoft.AspNetCore.Authorization;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class ContatoController : BaseController
    {
        IContatoService _contatoService;

        public ContatoController(IContatoService contatoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _contatoService = contatoService;
        }

        /// <summary>
        /// Gets all Contatos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<ContatoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<ContatoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _contatoService.GetAllSummariesAsync(), _contatoService);
        }

        /// <summary>
        /// Gets a Contato.
        /// <param name="id">Contato's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<ContatoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<ContatoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _contatoService.GetSummaryAsync(id), _contatoService);
        }

        /// <summary>
        /// Creates a new Cotnrato.
        /// </summary>
        /// <param name="ContatoSummary">Contato's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] ContatoSummary ContatoSummary)
        {
            var entity = await this._contatoService.CreateAsync(ContatoSummary);
            if (_contatoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_contatoService);
            }
            return await base.ResponseAsync(entity.Id, _contatoService);
        }

        /// <summary>
        /// Modifies an existing Contato.
        /// </summary>
        /// <param name="ContatoSUmmary">Modified Contato list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] ContatoSummary ContatoSUmmary)
        {
            return await base.ResponseAsync(await this._contatoService.UpdateAsync(ContatoSUmmary) != null, _contatoService);
        }

        /// <summary>
        /// Removes an existing Contato.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._contatoService.DeleteAsync(id, false), _contatoService);
        }
    }
}