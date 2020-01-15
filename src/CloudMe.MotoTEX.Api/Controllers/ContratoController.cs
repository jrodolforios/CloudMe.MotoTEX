using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Passageiro;
using Microsoft.AspNetCore.Cors;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using CloudMe.MotoTEX.Api.Models;
using CloudMe.MotoTEX.Domain.Model;
using Microsoft.AspNetCore.Authorization;

namespace CloudMe.MotoTEX.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class ContratoController : BaseController
    {
        IContratoService _contratoService;

        public ContratoController(IContratoService contratoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _contratoService = contratoService;
        }

        /// <summary>
        /// Gets all Contratos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<ContratoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<ContratoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _contratoService.GetAllSummariesAsync(), _contratoService);
        }

        /// <summary>
        /// Gets a Contrato.
        /// <param name="id">Contrato's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<ContratoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<ContratoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _contratoService.GetSummaryAsync(id), _contratoService);
        }

        /// <summary>
        /// Creates a new Cotnrato.
        /// </summary>
        /// <param name="ContratoSummary">Contrato's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] ContratoSummary ContratoSummary)
        {
            var entity = await this._contratoService.CreateAsync(ContratoSummary);
            if (_contratoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_contratoService);
            }
            return await base.ResponseAsync(entity.Id, _contratoService);
        }

        /// <summary>
        /// Modifies an existing Contrato.
        /// </summary>
        /// <param name="ContratoSUmmary">Modified Contrato list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] ContratoSummary ContratoSUmmary)
        {
            return await base.ResponseAsync(await this._contratoService.UpdateAsync(ContratoSUmmary) != null, _contratoService);
        }

        /// <summary>
        /// Removes an existing Contrato.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._contratoService.DeleteAsync(id, false), _contratoService);
        }

        /// <summary>
        /// Informa a localização de um Passageiro.
        /// </summary>
        /// <param name="localizacao">Sumário da nova localização do passageiro (necessário apenas latitude e longitude)</param>
        [HttpPost("Ultimo_contrato_valido")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<ContratoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<ContratoSummary>> UltimoContratoValido()
        {
            // atualiza o registro do passageiro
            return await ResponseAsync(await _contratoService.UltimoContratoValido(), _contratoService);
        }
    }
}